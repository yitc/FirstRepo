using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using System.Data.SqlTypes;
using BIS.Model;

namespace BIS.DAO
{
    public class PersonDAO
    {
        private dbConnection conn;
        public PersonDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetPerson(int idContPers)
        {
            string query = string.Format(@"
                SELECT idContPers, initialsContPers, firstname, midname, lastname, maidenname, idTitle, idGender, birthdate, dtCreated, 
                       idUserCreated, dtModified, idUserModified, idUserResponsible, isMaried, isActive, isDied, dtOfDeath, isNeedProspect,
                       isNeedMail,isPaperByMail,isContactPerson,idClient, livesIn,idCpFunction,isRequestBrochure,oldTripCount, travelInsurance, polisNumber, alarmNumber, idContPersBookingTo 
                FROM ContactPerson
                WHERE idContPers = @idContPers");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetPersonsButThis(int idContPers)
        {
            string query = string.Format(@"
                SELECT idContPers,  firstname, midname, lastname
                FROM ContactPerson
                WHERE idContPers <> @idContPers");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public object GetPersonImage(int idContPers)
        {
            string query = string.Format(@"
                SELECT imageContPers 
                FROM ContactPerson
                WHERE idContPers = @idContPers");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeScalarQuery(query, sqlParameters);
        }

        public DataTable GetPersonsNoFilter()
        {
            string query = string.Format(@"
                SELECT idContPers, initialsContPers, firstname, midname, lastname, maidenname
                FROM ContactPerson
                ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllPersons(int idFilter, List<int> labels, string idLang)
        {

            string query = string.Format(@"SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,oldTripCount, cp.travelInsurance, cp.polisNumber, cp.alarmNumber, 
                cpa.postalCode, cpa.city , cp.idContPersBookingTo
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN ContactPersonAddress cpa ON cpa.idContPers = cp.idContPers AND cpa.idAddressType = '1' 
                LEFT JOIN STRING" + idLang + " s ON g.nameGender=s.stringKey " +
                @"INNER JOIN ContactpersonFilter cpf ON cp.idContPers = cpf.idContPers
                WHERE cpf.idFilter = @idFilter");

            if(idFilter == 0)
            {                
                query  = query.Replace("WHERE cpf.idFilter = @idFilter","");
                query = query.Replace("INNER JOIN ContactpersonFilter cpf ON cp.idContPers = cpf.idContPers", "");
                
                if(labels.Count > 0)
                {
                    query += " INNER JOIN ContactPersonLabel cpl ON cp.idContPers = cpl.idContPers ";
                    query += "WHERE ";
                    query += "(";
                    int count = 0;
                    foreach(var idlabel in labels)
                    {

                        
                        if(count == 0)
                            query += "cpl.idLabel = '" + idlabel.ToString() +"' ";

                        if(count > 0)                        
                            query += " OR cpl.idLabel = '" + idlabel.ToString() + "' ";
                        

                        count++;
                    }
                    query += ")";
                }
               
                return conn.executeSelectQuery(query, null);
            }
            else
            {
               if(labels.Count > 0)
               {
                   query = query.Replace("WHERE cpf.idFilter = @idFilter", "");                   
                   query += " INNER JOIN ContactPersonLabel cpl ON cp.idContPers = cpl.idContPers ";
                   query += "WHERE ";
                   query += "cpf.idFilter = @idFilter AND ";
                   query += "(";

                   int count = 0;
                   foreach (var idlabel in labels)
                   {


                       if (count == 0)
                           query += "cpl.idLabel = '" + idlabel.ToString() + "' ";

                       if (count > 0)
                           query += " OR cpl.idLabel = '" + idlabel.ToString() + "' ";


                       count++;
                   }
                   query += ")";

               }
                
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@idFilter", SqlDbType.Int);
                sqlParameters[0].Value = idFilter;
                return conn.executeSelectQuery(query, sqlParameters);
            }

            

            
        }

        public DataTable GetFiltersPerson(int idContPers)
        {

            string query = string.Format(@"SELECT idContPers,idFilter
                FROM ContactPersonFilter 
                WHERE idContPers = @idContPers");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetLabelsPerson(int idContPers)
        {

            string query = string.Format(@"SELECT idContPers,idLabel
                FROM ContactPersonLabel 
                WHERE idContPers = @idContPers");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetLastPersonID()
        {
            string query = string.Format(@"SELECT TOP 1 idContPers FROM ContactPerson ORDER BY idContPers DESC");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Save(PersonModel person)
        {
            string query = string.Format(@"INSERT INTO 
                    ContactPerson(initialsContPers, firstname, midname, lastname, maidenname, idTitle, idGender, birthdate, dtCreated, 
                    idUserCreated, dtModified, idUserModified, idUserResponsible, isMaried, isActive, isDied, dtOfDeath, isNeedProspect,
                    isNeedMail, imageContPers,identBSN,isPayInvoice,isSharePicture,isPaperByMail,isContactPerson,idClient, livesIn,idCpFunction,
                    isRequestBrochure, idReasonIn,idReasonOut, volProfession, travelInsurance, polisNumber, alarmNumber, idContPersBookingTo) 
                    VALUES (@initialsContPers, @firstname, @midname, @lastname, @maidenname, @idTitle, @idGender, @birthdate, @dtCreated, 
                    @idUserCreated, @dtModified, @idUserModified, @idUserResponsible, @isMaried, @isActive, @isDied, @dtOfDeath, @isNeedProspect, 
                    @isNeedMail, @imageContPers,@identBSN,@isPayInvoice,@isSharePicture,@isPaperByMail,@isContactPerson,@idClient, @livesIn,@idCpFunction,
                    @isRequestBrochure,@idReasonIn,@idReasonOut, @volProfession, @travelInsurance, @polisNumber, @alarmNumber, @idContPersBookingTo )");


            SqlParameter[] sqlParameters = new SqlParameter[36];

            sqlParameters[0] = new SqlParameter("@initialsContPers", SqlDbType.NVarChar);
            sqlParameters[0].Value = person.initialsContPers;

            sqlParameters[1] = new SqlParameter("@firstname", SqlDbType.NVarChar);
            sqlParameters[1].Value = person.firstname;

            sqlParameters[2] = new SqlParameter("@midname", SqlDbType.NVarChar);
            sqlParameters[2].Value = person.midname;

            sqlParameters[3] = new SqlParameter("@lastname", SqlDbType.NVarChar);
            sqlParameters[3].Value = person.lastname;

            sqlParameters[4] = new SqlParameter("@maidenname", SqlDbType.NVarChar);
            sqlParameters[4].Value = person.maidenname;

            sqlParameters[5] = new SqlParameter("@idTitle", SqlDbType.Int);
            sqlParameters[5].Value = person.idTitle;

            sqlParameters[6] = new SqlParameter("@idGender", SqlDbType.Int);
            //sqlParameters[6].Value = (person.idGender == null) ? SqlInt32.Null : person.idGender;
            sqlParameters[6].Value = person.idGender;

            sqlParameters[7] = new SqlParameter("@birthdate", SqlDbType.DateTime);
            sqlParameters[7].Value = (person.birthdate == null || person.birthdate == DateTime.MinValue) ? SqlDateTime.Null : person.birthdate;

            sqlParameters[8] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameters[8].Value = (person.dtCreated == null || person.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : person.dtCreated;

            sqlParameters[9] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameters[9].Value = person.idUserCreated;

            sqlParameters[10] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameters[10].Value = (person.dtModified == null || person.dtModified == DateTime.MinValue) ? SqlDateTime.Null : person.dtModified;

            sqlParameters[11] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameters[11].Value = person.idUserModified;

            sqlParameters[12] = new SqlParameter("@idUserResponsible", SqlDbType.Int);
            sqlParameters[12].Value = person.idUserResponsible;

            sqlParameters[13] = new SqlParameter("@isMaried", SqlDbType.Bit);
            sqlParameters[13].Value = person.isMaried;

            sqlParameters[14] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameters[14].Value = person.isActive;

            sqlParameters[15] = new SqlParameter("@isDied", SqlDbType.Bit);
            sqlParameters[15].Value = person.isDied;

            sqlParameters[16] = new SqlParameter("@dtOfDeath", SqlDbType.DateTime);
            sqlParameters[16].Value = (person.dtOfDeath == null || person.dtOfDeath == DateTime.MinValue) ? SqlDateTime.Null : person.dtOfDeath;

            sqlParameters[17] = new SqlParameter("@isNeedProspect", SqlDbType.Int);
            sqlParameters[17].Value = person.isNeedProspect;

            sqlParameters[18] = new SqlParameter("@isNeedMail", SqlDbType.Int);
            sqlParameters[18].Value = person.isNeedMail;

            sqlParameters[19] = new SqlParameter("@imageContPers", SqlDbType.NVarChar);
            sqlParameters[19].Value = (person.imageContPers == null) ? SqlString.Null : person.imageContPers;

            sqlParameters[20] = new SqlParameter("@identBSN", SqlDbType.NVarChar);
            sqlParameters[20].Value = (person.identBSN == null) ? SqlString.Null : person.identBSN;

            sqlParameters[21] = new SqlParameter("@isPayInvoice", SqlDbType.Bit);
            sqlParameters[21].Value = person.isPayInvoice;

            sqlParameters[22] = new SqlParameter("@isSharePicture", SqlDbType.Bit);
            sqlParameters[22].Value = person.isSharePicture;

            sqlParameters[23] = new SqlParameter("@isPaperByMail", SqlDbType.Bit);
            sqlParameters[23].Value = person.isPaperByMail;

            sqlParameters[24] = new SqlParameter("@isContactPerson", SqlDbType.Bit);
            sqlParameters[24].Value = person.isContactPerson;

            sqlParameters[25] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[25].Value = person.idClient;

            sqlParameters[26] = new SqlParameter("@livesIn", SqlDbType.Int);
            sqlParameters[26].Value = person.livesIn;

            sqlParameters[27] = new SqlParameter("@idCpFunction", SqlDbType.Int);
            sqlParameters[27].Value = person.idCpFunction;

            sqlParameters[28] = new SqlParameter("@isRequestBrochure", SqlDbType.Bit);
            sqlParameters[28].Value = person.isRequestBrochure;

            sqlParameters[29] = new SqlParameter("@idReasonIn", SqlDbType.Int);
            sqlParameters[29].Value = person.idReasonIn;

            sqlParameters[30] = new SqlParameter("@idReasonOut", SqlDbType.Int);
            sqlParameters[30].Value = person.idReasonOut;

            sqlParameters[31] = new SqlParameter("@volProfession", SqlDbType.NVarChar);
            sqlParameters[31].Value = person.volProfession;

            sqlParameters[32] = new SqlParameter("@travelInsurance", SqlDbType.NVarChar);
            sqlParameters[32].Value = person.travelInsurance;

            sqlParameters[33] = new SqlParameter("@polisNumber", SqlDbType.NVarChar);
            sqlParameters[33].Value = person.polisNumber;

            sqlParameters[34] = new SqlParameter("@alarmNumber", SqlDbType.NVarChar);
            sqlParameters[34].Value = person.alarmNumber;

            sqlParameters[35] = new SqlParameter("@idContPersBookingTo", SqlDbType.NVarChar);
            sqlParameters[35].Value = person.idContPersBookingTo;

            

            return conn.executeInsertQuery(query, sqlParameters);
        }

        public bool Update(PersonModel person)
        {
            string query = string.Format(@"UPDATE ContactPerson SET  
                    initialsContPers = @initialsContPers, firstname = @firstname, midname = @midname, lastname = @lastname, maidenname = @maidenname, 
                    idTitle = @idTitle, idGender = @idGender, birthdate = @birthdate, dtCreated = @dtCreated, 
                    idUserCreated = idUserCreated, dtModified = @dtModified, idUserModified = @idUserModified, idUserResponsible = @idUserResponsible, 
                    isMaried = @isMaried, isActive = @isActive, isDied = @isDied, dtOfDeath = @dtOfDeath, isNeedProspect = @isNeedProspect,
                    isNeedMail = @isNeedMail, imageContPers = @imageContPers , identBSN = @identBSN, isPayInvoice = @isPayInvoice, isSharePicture = @isSharePicture,
                    isPaperByMail=@isPaperByMail,isContactPerson=@isContactPerson,idClient=@idClient, livesIn=@livesIn,idCpFunction=@idCpFunction, isRequestBrochure=@isRequestBrochure,
                    idReasonIn=@idReasonIn,idReasonOut=@idReasonOut,volProfession=@volProfession, 
                    travelInsurance = @travelInsurance, polisNumber = @polisNumber, alarmNumber = @alarmNumber , idContPersBookingTo = @idContPersBookingTo
                    WHERE idContPers = @idContPers");

            SqlParameter[] sqlParameters = new SqlParameter[37];

            sqlParameters[0] = new SqlParameter("@initialsContPers", SqlDbType.NVarChar);
            sqlParameters[0].Value = person.initialsContPers;

            sqlParameters[1] = new SqlParameter("@firstname", SqlDbType.NVarChar);
            sqlParameters[1].Value = person.firstname;

            sqlParameters[2] = new SqlParameter("@midname", SqlDbType.NVarChar);
            sqlParameters[2].Value = person.midname;

            sqlParameters[3] = new SqlParameter("@lastname", SqlDbType.NVarChar);
            sqlParameters[3].Value = person.lastname;

            sqlParameters[4] = new SqlParameter("@maidenname", SqlDbType.NVarChar);
            sqlParameters[4].Value = person.maidenname;

            sqlParameters[5] = new SqlParameter("@idTitle", SqlDbType.Int);
            sqlParameters[5].Value = person.idTitle;

            sqlParameters[6] = new SqlParameter("@idGender", SqlDbType.Int);
            sqlParameters[6].Value = person.idGender;

            sqlParameters[7] = new SqlParameter("@birthdate", SqlDbType.DateTime);
            sqlParameters[7].Value = (person.birthdate == null || person.birthdate == DateTime.MinValue) ? SqlDateTime.Null : person.birthdate;

            sqlParameters[8] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameters[8].Value = (person.dtCreated == null || person.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : person.dtCreated;

            sqlParameters[9] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameters[9].Value = person.idUserCreated;

            sqlParameters[10] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameters[10].Value = (person.dtModified == null || person.dtModified == DateTime.MinValue) ? SqlDateTime.Null : person.dtModified;

            sqlParameters[11] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameters[11].Value = person.idUserModified;

            sqlParameters[12] = new SqlParameter("@idUserResponsible", SqlDbType.Int);
            sqlParameters[12].Value = person.idUserResponsible;

            sqlParameters[13] = new SqlParameter("@isMaried", SqlDbType.Bit);
            sqlParameters[13].Value = person.isMaried;

            sqlParameters[14] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameters[14].Value = person.isActive;

            sqlParameters[15] = new SqlParameter("@isDied", SqlDbType.Bit);
            sqlParameters[15].Value = person.isDied;

            sqlParameters[16] = new SqlParameter("@dtOfDeath", SqlDbType.DateTime);
            sqlParameters[16].Value = (person.dtOfDeath == null || person.dtOfDeath == DateTime.MinValue) ? SqlDateTime.Null : person.dtOfDeath;

            sqlParameters[17] = new SqlParameter("@isNeedProspect", SqlDbType.Int);
            sqlParameters[17].Value = person.isNeedProspect;

            sqlParameters[18] = new SqlParameter("@isNeedMail", SqlDbType.Int);
            sqlParameters[18].Value = person.isNeedMail;

            sqlParameters[19] = new SqlParameter("@imageContPers", SqlDbType.NVarChar);
            sqlParameters[19].Value = person.imageContPers;

            sqlParameters[20] = new SqlParameter("@identBSN", SqlDbType.NVarChar);
            sqlParameters[20].Value = (person.identBSN == null) ? SqlString.Null : person.identBSN;

            sqlParameters[21] = new SqlParameter("@isPayInvoice", SqlDbType.Bit);
            sqlParameters[21].Value = person.isPayInvoice;

            sqlParameters[22] = new SqlParameter("@isSharePicture", SqlDbType.Bit);
            sqlParameters[22].Value = person.isSharePicture;

            sqlParameters[23] = new SqlParameter("@isPaperByMail", SqlDbType.Bit);
            sqlParameters[23].Value = person.isPaperByMail;

            sqlParameters[24] = new SqlParameter("@isContactPerson", SqlDbType.Bit);
            sqlParameters[24].Value = person.isContactPerson;

            sqlParameters[25] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[25].Value = person.idClient;

            sqlParameters[26] = new SqlParameter("@livesIn", SqlDbType.Int);
            sqlParameters[26].Value = person.livesIn;

            sqlParameters[27] = new SqlParameter("@idCpFunction", SqlDbType.Int);
            sqlParameters[27].Value = person.idCpFunction;

            sqlParameters[28] = new SqlParameter("@isRequestBrochure", SqlDbType.Bit);
            sqlParameters[28].Value = person.isRequestBrochure;

            sqlParameters[29] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[29].Value = person.idContPers;

            sqlParameters[30] = new SqlParameter("@idReasonIn", SqlDbType.Int);
            sqlParameters[30].Value = person.idReasonIn;

            sqlParameters[31] = new SqlParameter("@idReasonOut", SqlDbType.Int);
            sqlParameters[31].Value = person.idReasonOut;

            sqlParameters[32] = new SqlParameter("@volProfession", SqlDbType.NVarChar);
            sqlParameters[32].Value = person.volProfession;

            sqlParameters[33] = new SqlParameter("@travelInsurance", SqlDbType.NVarChar);
            sqlParameters[33].Value = person.travelInsurance;

            sqlParameters[34] = new SqlParameter("@polisNumber", SqlDbType.NVarChar);
            sqlParameters[34].Value = person.polisNumber;

            sqlParameters[35] = new SqlParameter("@alarmNumber", SqlDbType.NVarChar);
            sqlParameters[35].Value = person.alarmNumber;

            sqlParameters[36] = new SqlParameter("@idContPersBookingTo", SqlDbType.NVarChar);
            sqlParameters[36].Value = person.idContPersBookingTo;

            return conn.executeUpdateQuery(query, sqlParameters);

        }

        public DataTable GetVHPersons(int idArrangement, int idContPers, string idLang, List<int> CheckedFun, List<int> CheckedQuest, List<int> CheckedTrip, List<int> CheckedTripQuest, List<int> CheckedVol, List<int> CheckedVolQuest)
        {
            List<string> tableNameList = new List<string>();

            List<string> tableLongNameList = new List<string>();

            if (CheckedFun.Count > 0)
            {
                tableLongNameList.Add("VolFunctionCpr");
                tableNameList.Add("vcp");
            }
            if (CheckedTrip.Count > 0)
            {
                tableLongNameList.Add("VolTripCpr");
                tableNameList.Add("vtcp");
            }
            if (CheckedVol.Count > 0)
            {
                tableLongNameList.Add("VolCpr");
                tableNameList.Add("vcpr");
            }

            string condition1 = "";
            string condition2 = "";
            string condition3 = "";

            List<int> newList = new List<int>();
            List<int> newListQuest = new List<int>();
            if (tableNameList.Count >= 1)
            {

                if (tableNameList[0] == "vcp")
                {
                    newList = CheckedFun;
                    newListQuest = CheckedQuest;
                }

                if (tableNameList[0] == "vtcp")
                {
                    newList = CheckedTrip;
                    newListQuest = CheckedTripQuest;
                }

                if (tableNameList[0] == "vcpr")
                {
                    newList = CheckedVol;
                    newListQuest = CheckedVolQuest;
                }

                for (int i = 0; i < newList.Count; i++)
                {
                    if (i == 0)
                    {
                        condition1 = "AND (" + tableNameList[0] + ".idAns='" + newList[i].ToString() + "' AND " + tableNameList[0] + ".idQuest ='" + newListQuest[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition1 = condition1 + " OR (" + tableNameList[0] + ".idAns='" + newList[i].ToString() + "' AND " + tableNameList[0] + ".idQuest ='" + newListQuest[i].ToString() + "')";
                    }
                }

                if (newList.Count > 1)
                    condition1 = "AND (" + condition1.Substring(3, condition1.Length - 3) + ")";
            }

            List<int> newList2 = new List<int>();
            List<int> newListQuest2 = new List<int>();
            if (tableNameList.Count >= 2)
            {

                if (tableNameList[1] == "vcp")
                {
                    newList2 = CheckedFun;
                    newListQuest2 = CheckedQuest;
                }

                if (tableNameList[1] == "vtcp")
                {
                    newList2 = CheckedTrip;
                    newListQuest2 = CheckedTripQuest;
                }

                if (tableNameList[1] == "vcpr")
                {
                    newList2 = CheckedVol;
                    newListQuest2 = CheckedVolQuest;
                }


                for (int i = 0; i < newList2.Count; i++)
                {
                    if (i == 0)
                    {
                        condition2 = "WHERE (" + tableNameList[1] + ".idAns='" + newList2[i].ToString() + "' AND " + tableNameList[1] + ".idQuest ='" + newListQuest2[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition2 = condition2 + " OR (" + tableNameList[1] + ".idAns='" + newList2[i].ToString() + "' AND " + tableNameList[1] + ".idQuest ='" + newListQuest2[i].ToString() + "')";
                    }
                }

            }

            List<int> newList3 = new List<int>();
            List<int> newListQuest3 = new List<int>();
            if (tableNameList.Count == 3)
            {

                if (tableNameList[2] == "vcp")
                {
                    newList3 = CheckedFun;
                    newListQuest3 = CheckedQuest;
                }

                if (tableNameList[2] == "vtcp")
                {
                    newList3 = CheckedTrip;
                    newListQuest3 = CheckedTripQuest;
                }

                if (tableNameList[2] == "vcpr")
                {
                    newList3 = CheckedVol;
                    newListQuest3 = CheckedVolQuest;
                }


                for (int i = 0; i < newList3.Count; i++)
                {
                    if (i == 0)
                    {
                        condition3 = "WHERE (" + tableNameList[2] + ".idAns='" + newList3[i].ToString() + "' AND " + tableNameList[2] + ".idQuest ='" + newListQuest3[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition3 = condition3 + " OR (" + tableNameList[2] + ".idAns='" + newList3[i].ToString() + "' AND " + tableNameList[2] + ".idQuest ='" + newListQuest3[i].ToString() + "')";
                    }
                }

            }

            string query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement WHERE  ab.idStatus<>'3' AND (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                (a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers IN (SELECT DISTINCT idContPers FROM VolAvailability WHERE (SELECT DATEADD(dd,-3,dtFromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')>=dateFrom AND (SELECT DATEADD(dd,+3,dtToArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')<=dateTo)
                AND idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') ");

            if (tableNameList.Count == 1)
            {
                query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[0] + @".idVol)
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN " + tableLongNameList[0] + @" " + tableNameList[0] + @" ON " + tableNameList[0] + @".idcpr = idContPers
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                 AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement WHERE  ab.idStatus<>'3' AND (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                (a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers IN (SELECT DISTINCT idContPers FROM VolAvailability WHERE (SELECT DATEADD(dd,-3,dtFromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')>=dateFrom AND (SELECT DATEADD(dd,+3,dtToArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')<=dateTo)
                AND idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') " + condition1 +
                @" GROUP BY  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[0] + @".idVol)='" + newList.Count + "'");
            }
            else if (tableNameList.Count == 2)
            {
                query = string.Format(@"
                SELECT  idContPers,initialsContPers,firstname,midname,lastname,maidenname,idTitle,nameTitle,idGender,nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[1] + @".idVol)
                FROM (
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[0] + @".idVol) as cc
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN " + tableLongNameList[0] + @" " + tableNameList[0] + @" ON " + tableNameList[0] + @".idcpr = idContPers
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement WHERE  ab.idStatus<>'3' AND (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                (a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers IN (SELECT DISTINCT idContPers FROM VolAvailability WHERE (SELECT DATEADD(dd,-3,dtFromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')>=dateFrom AND (SELECT DATEADD(dd,+3,dtToArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')<=dateTo)
                AND idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') " + condition1 +
                @" GROUP BY  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[0] + @".idVol)='" + newList.Count + @"') as dd
                LEFT OUTER JOIN " + tableLongNameList[1] + @" " + tableNameList[1] + @" ON " + tableNameList[1] + @".idcpr = dd.idContPers " + condition2 + @"
                GROUP BY  dd.idContPers,initialsContPers,firstname,midname,lastname,maidenname,idTitle,nameTitle,idGender,nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[1] + @".idVol)='" + newList2.Count + @"'");
            }
            else if (tableNameList.Count == 3)
            {
                query = string.Format(@"
                SELECT  idContPers,initialsContPers,firstname,midname,lastname,maidenname,idTitle,nameTitle,idGender,nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[2] + @".idVol)
                FROM
                (SELECT  idContPers,initialsContPers,firstname,midname,lastname,maidenname,idTitle,nameTitle,idGender,nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[1] + @".idVol) as ccc
                FROM (
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[0] + @".idVol) as cc
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN " + tableLongNameList[0] + @" " + tableNameList[0] + @" ON " + tableNameList[0] + @".idcpr = idContPers
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement  WHERE ab.idStatus<>'3' AND (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                ( a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers IN (SELECT DISTINCT idContPers FROM VolAvailability WHERE (SELECT DATEADD(dd,-3,dtFromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')>=dateFrom AND (SELECT DATEADD(dd,+3,dtToArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')<=dateTo)
                AND idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') " + condition1 +
                @" GROUP BY  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[0] + @".idVol)='" + newList.Count + @"') as dd
                LEFT OUTER JOIN " + tableLongNameList[1] + @" " + tableNameList[1] + @" ON " + tableNameList[1] + @".idcpr = dd.idContPers " + condition2 + @"
                GROUP BY  dd.idContPers,initialsContPers,firstname,midname,lastname,maidenname,idTitle,nameTitle,idGender,nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[1] + @".idVol)='" + newList2.Count + @"') as ddd 
                LEFT OUTER JOIN " + tableLongNameList[2] + @" " + tableNameList[2] + @" ON " + tableNameList[2] + @".idcpr = ddd.idContPers " + condition3 + @"
                GROUP BY  ddd.idContPers,initialsContPers,firstname,midname,lastname,maidenname,idTitle,nameTitle,idGender,nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[2] + @".idVol)='" + newList3.Count + @"'");
            }

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementPersonsLookup(int idArrangement, int idContPers, string idLang)
        {
            string query = string.Format(@"
                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                WHERE idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + "' ANd idContPers<>'" + idContPers + @"')
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"' AND idArrangementBook IN (SELECT DISTINCT idArrangementBook FROM ArrangementBookPersons))
                AND idContPers NOT IN (SELECT DISTINCT apb.idContPers FROM ArrangementBookPersons apb INNER JOIN ArrangementBook ab ON ab.idArrangementBook=apb.idArrangementBook WHERE ab.idArrangement = '" + idArrangement + @"')
                AND idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') 
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"' AND idStatus = '2')
                UNION
                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                RIGHT OUTER JOIN (SELECT DISTINCT apb.idContPers FROM ArrangementBookPersons apb INNER JOIN ArrangementBook ab ON ab.idArrangementBook=apb.idArrangementBook WHERE ab.idArrangement = '" + idArrangement + @"' and ab.idContPers='" + idContPers + @"') acc ON acc.idContPers = cp.idContPers");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementVHPersonsLookup(int idArrangement, int idContPers, string idLang)
        {
            string query = string.Format(@"
                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                WHERE idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + "' ANd idContPers<>'" + idContPers + @"')
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"' AND idArrangementBook IN (SELECT DISTINCT idArrangementBook FROM ArrangementBookPersons))
                AND idContPers NOT IN (SELECT DISTINCT apb.idContPers FROM ArrangementBookPersons apb INNER JOIN ArrangementBook ab ON ab.idArrangementBook=apb.idArrangementBook WHERE ab.idArrangement = '" + idArrangement + @"')
                AND idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') 
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"' AND idStatus = '2')
                UNION
                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                RIGHT OUTER JOIN (SELECT DISTINCT apb.idContPers FROM ArrangementBookPersons apb INNER JOIN ArrangementBook ab ON ab.idArrangementBook=apb.idArrangementBook WHERE ab.idArrangement = '" + idArrangement + @"' and ab.idContPers='" + idContPers + @"') acc ON acc.idContPers = cp.idContPers");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementPersons(int idArrangementBook, string idLang)
        {
            string query = string.Format(@"
                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                WHERE idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBookPersons  WHERE idArrangementBook = '" + idArrangementBook + @"') 
                AND idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }


        public DataTable GetArrangementVHPersons(int idArrangementBook, string idLang)
        {
            string query = string.Format(@"
                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                WHERE idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBookPersons  WHERE idArrangementBook = '" + idArrangementBook + @"') 
                AND idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllPersonsForArrangement(int idArrangement, string idLang)
        {
            string query = string.Format(@"
               SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.idStatus,pp.nameStatus,pp.dtBooked,'Traveler' as type,isInsurance
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.idStatus,abst.nameStatus,ab.dtBooked,isInsurance FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus WHERE ab.idArrangement = '" + idArrangement + @"') pp ON pp.idContPers = cp.idContPers
                WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook mm WHERE idArrangement = '" + idArrangement + @"')
                AND cp.idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')
                UNION
                SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.idStatus,pp.nameStatus,pp.dtBooked,'Voluntary helper' as type,isInsurance
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.idStatus,abst.nameStatus,ab.dtBooked,isInsurance FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus WHERE ab.idArrangement = '" + idArrangement + @"') pp ON pp.idContPers = cp.idContPers
                WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"')
                AND cp.idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }
       
//      
         public DataTable GetPersonsVHForArrangement(int idArrangement, string idLang)
         {
             string query = string.Format(@"
                 SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.nameStatus
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.idStatus,abst.nameStatus FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus WHERE ab.idArrangement = '" + idArrangement + @"') pp ON pp.idContPers = cp.idContPers
                WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"')
                AND pp.idStatus <> '3' AND cp.idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

             return conn.executeSelectQuery(query, null);
         }

//     
        //======= Getting Male Vol Helpers
         public DataTable GetMalePersonsVHForArrangement(int idArrangement, string idLang)
         {
             string query = string.Format(@"
                 SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.nameStatus
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.idStatus,abst.nameStatus FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus WHERE ab.idArrangement = '" + idArrangement + @"') pp ON pp.idContPers = cp.idContPers
                WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"')
                AND pp.idStatus <> '3' AND cp.idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')and cp.idGender = '1'");

             return conn.executeSelectQuery(query, null);
         }
//        
        //=================================

         public DataTable GetAllArrangement(int idContPers)
         {

             string query = string.Format(@"SELECT DISTINCT a.idArrangement,codeArrangement,nameArrangement, dtFromArrangement,dtToArrangement,nrOfNights,
                                        cityArrangement,countryArrangement,c.nameCountry as countryNameArrangement,typeArrangement,t.nameArrType as typeNameArrangement,
                                        nrTraveler, minNrTraveler, nrVoluntaryHelper, a.idHotelService, h.nameHotelService,
                                        isWeb, nrMaleVoluntary, a.idAgeCategory, cat.descAgeCategory , nrMaximumWheelchairs, whoseElectricWheelchairs,
                                        buRollators,buSupportingArms,statusArrangement,(SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement) as booked,
                                        (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 1 and a.idArrangement=m.idArrangement) as optionalBooked,
										(nrTraveler - (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement)) as freePlaces,
                                        CASE WHEN ac.price IS NULL THEN 0 ELSE ac.price END AS price,  daysFirstPayment,daysLastPayment,percentFirstPayment,
                                        reservationCosts,nrAnchorage
										 FROM Arrangement a 
                                        LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                        LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType
                                        LEFT OUTER JOIN HotelService h on h.idHotelService = a.idHotelService
                                        Left  OUTER JOIN ArrangementBook m on a.idArrangement = m.idArrangement
                                        Left  OUTER JOIN ArrangementBook s on a.idArrangement = s.idArrangement
                                        Left  OUTER JOIN ArrangementCalculation ac on a.idArrangement = ac.idArrangement
                                        Left  OUTER JOIN AgeCategory cat on a.idAgeCategory = cat.idAgeCategory
                                         WHERE a.idArrangement NOT IN (SELECT DISTINCT idArrangement FROM ArrangementBook WHERE idContPers= @idContPers)");


             SqlParameter[] sqlParameters = new SqlParameter[1];

             sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
             sqlParameters[0].Value = (idContPers);
             return conn.executeSelectQuery(query, sqlParameters);


         }

        public DataTable GetArrangementsForPerson(int idPerson)
        {
            string query = string.Format(@"
                SELECT ab.idArrangement, a.codeArrangement, a.nameArrangement, a.dtFromArrangement,a.dtToArrangement,
                                        a.cityArrangement,a.countryArrangement,c.nameCountry as countryNameArrangement,typeArrangement,t.nameArrType as typeNameArrangement,
                                        a.nrTraveler, a.minNrTraveler, a.nrVoluntaryHelper,ab.idArrangementBook, ab.idStatus,abs.nameStatus AS nameStatus, ab.idTravelPapers,abp.nameTravelPapers,ab.price
                                        FROM ArrangementBook ab
                                        Left outer join Arrangement a on ab.idArrangement= a.idArrangement
                                        LEFT OUTER JOIN Country c on a.countryArrangement = c.idCountry
                                        LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType
                                        LEFT OUTER JOIN ArrangementBookStatus abs on ab.idStatus = abs.idStatus
                                        LEFT OUTER JOIN ArrangementBookTravelPapers abp on ab.idTravelPapers = abp.idTravelPapers
                WHERE idContPers=@idPerson ");
            // AND idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idPerson", SqlDbType.Int);
            sqlParameters[0].Value = (idPerson);
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable IsPersonVolHelp()
        {
            string query = string.Format(@"SELECT DISTINCT idContPers AS ID FROM ContactPersonFilter WHERE idFilter = '4'");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable NrVolontary(int idArrangement)
        {
            string query = string.Format(@"SELECT nrVoluntaryHelper - (SELECT COUNT(idContPers)
            FROM ArrangementBook
            WHERE idArrangement = @idArrangement and idContPers IN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4' )) AS ID
            FROM Arrangement
            WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangement);
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable NrTraveler(int idArrangement)
        {
            string query = string.Format(@"SELECT nrTraveler - (SELECT COUNT(idContPers)
            FROM ArrangementBook
            WHERE idArrangement = @idArrangement and idContPers IN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter <> '4' )) AS ID
            FROM Arrangement
            WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangement);
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable IsAlreadyTraveling(int idArrangement, int idContpers)
        {
            string query = string.Format(@"SELECT idArrangementBook, idArrangement, idContPers, idStatus, idTravelPapers, price
            FROM ArrangementBook
            WHERE idArrangement = @idArrangement and idContPers=@idContpers");
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangement);

            sqlParameters[1] = new SqlParameter("@idContpers", SqlDbType.Int);
            sqlParameters[1].Value = (idContpers);

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable CountNrTraveling(int idContpers)
        {
            string query = string.Format(@"SELECT Count(idContPers)+ (SELECT oldTripCount FROM ContactPerson WHERE idContPers=@idContpers) as br FROM ArrangementBook a
            LEFT JOIN Arrangement e on e.idArrangement = a.idArrangement
            WHERE  idContPers=@idContpers and idStatus < 3 and LEFT(e.codeArrangement,3) != 'TRC'
            AND idContPers=@idContpers and idStatus < 3 and LEFT(e.codeArrangement,3) != 'BHC' AND dtBooked >= '03-29-2016 00:00:000'");
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idContpers", SqlDbType.Int);
            sqlParameters[0].Value = (idContpers);

          
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool SaveArrangement(int idArrangement, int idContPers, int idStatus, int idTravelPapers, decimal price)
        {
            string query = string.Format(@"INSERT INTO 
                    ArrangementBook(idArrangement,idContPers,idStatus,idTravelPapers,price) 
                    VALUES (@idArrangement,@idContPers,@idStatus,@idTravelPapers,@price)");


            SqlParameter[] sqlParameters = new SqlParameter[5];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[1].Value = idContPers;

            sqlParameters[2] = new SqlParameter("@idStatus", SqlDbType.Int);
            sqlParameters[2].Value = (idStatus == -1) ? SqlInt32.Null : idStatus;

            sqlParameters[3] = new SqlParameter("@idTravelPapers", SqlDbType.Int);
            sqlParameters[3].Value = (idTravelPapers == -1) ? SqlInt32.Null : idTravelPapers;

            sqlParameters[4] = new SqlParameter("@price", SqlDbType.Decimal);
            sqlParameters[4].Value = (price == -1) ? SqlDecimal.Null : price;


            return conn.executeInsertQuery(query, sqlParameters);

        }

        public DataTable checkIsInDebitor(int idContPers)
        {
            string query = string.Format(@"
                SELECT * FROM AccDebCre 
                WHERE idContPerson = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInArrangementBook(int idContPers)
        {
            string query = string.Format(@"
                SELECT * FROM ArrangementBook 
                WHERE idContPers = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInBISAppointment(int idContPers)
        {
            string query = string.Format(@"
                SELECT * FROM BISAppointment 
                WHERE contact = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInClientPerson(int idContPers)
        {
            string query = string.Format(@"
                SELECT * FROM ClientPerson 
                WHERE idContPerson = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInDocuments(int idContPers)
        {
            string query = string.Format(@"
                SELECT * FROM Documents 
                WHERE idContPers = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInInvoice(int idContPers)
        {
            string query = string.Format(@"
                SELECT * FROM Invoice 
                WHERE idContPerson = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean DeletePerson(int idContPers)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ContactPerson WHERE idContPers = @idContPers ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[0].Value = idContPers;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonAddress WHERE idContPers = @idContPers ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonEmail WHERE idContPers = @idContPers ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonFilter WHERE idContPers = @idContPers ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonLabel WHERE idContPers = @idContPers ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonNotes WHERE idContPers = @idContPers ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonPassport WHERE idContPers = @idContPers ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonTargetGroup WHERE idContPers = @idContPers ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ContactPersonTel WHERE idContPers = @idContPers ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            
            query = string.Format(@"DELETE FROM MedCpr WHERE idcpr = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);
            
            
            query = string.Format(@"DELETE FROM VolAvailability WHERE idContPers = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolCpr WHERE idcpr = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolFeatures WHERE idContPers = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolFunctionCpr WHERE idcpr = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolSimilarity WHERE idContPers = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolSimilarityArchive WHERE idContPers = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolTripCpr WHERE idcpr = @idContPers ");
            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable GetTravelers(int idArrangement, int idContPers, string idLang, List<string> CheckedCity, List<string> CheckedQuest, List<int> CheckedMedical, List<int> CheckedMedicalQuest)
        {
            List<string> tableNameList = new List<string>();

            List<string> tableLongNameList = new List<string>();

            if (CheckedMedical.Count > 0)
            {
                tableLongNameList.Add("MedCpr");
                tableNameList.Add("mcp");
            }

            string condition1 = "";
            string condition2 = "";

            List<int> newList = new List<int>();
            List<int> newListQuest = new List<int>();




            if (tableNameList.Count == 1)
            {

                newList = CheckedMedical;
                newListQuest = CheckedMedicalQuest;
                for (int i = 0; i < newList.Count; i++)
                {
                    if (i == 0)
                    {
                        condition1 = "AND (" + tableNameList[0] + ".idAns='" + CheckedMedical[i].ToString() + "' AND " + tableNameList[0] + ".idQuest ='" + CheckedMedicalQuest[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition1 = condition1 + " OR (" + tableNameList[0] + ".idAns='" + CheckedMedical[i].ToString() + "' AND " + tableNameList[0] + ".idQuest ='" + CheckedMedicalQuest[i].ToString() + "')";
                    }
                }

                if (newList.Count > 1)
                    condition1 = "AND (" + condition1.Substring(3, condition1.Length - 3) + ")";

            }

            if (CheckedCity.Count > 0)
            {
                for (int j = 0; j < CheckedCity.Count; j++)
                {
                    if (j == 0)
                    {
                        condition2 = "AND adr.City = '" + CheckedCity[j].Replace("'", "''") + "'";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition2 = condition2 + " OR adr.City = '" + CheckedCity[j].Replace("'", "''") + "'";
                    }
                }

                if (CheckedCity.Count > 1)
                    condition2 = "AND (" + condition2.Substring(3, condition2.Length - 3) + ")";
            }


            string query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement WHERE (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                (a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') ");

            if (tableNameList.Count == 1 && CheckedCity.Count <= 0)
            {
                query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[0] + @".idMed)
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN " + tableLongNameList[0] + @" " + tableNameList[0] + @" ON " + tableNameList[0] + @".idcpr = idContPers
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                 AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement WHERE (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                (a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') " + condition1 +
                @" GROUP BY  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[0] + @".idMed)='" + newList.Count + "'");
            }

            else if (tableNameList.Count < 1 && CheckedCity.Count > 0)
            {
                query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement WHERE (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                (a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') 
                AND idContPers IN (SELECT DISTINCT idContPers FROM ContactPersonAddress adr WHERE adr.idAddressType = '1' " + condition2 + ")");
            }

            else if (tableNameList.Count == 1 && CheckedCity.Count > 0)
            {
                query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[0] + @".idMed)
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN " + tableLongNameList[0] + @" " + tableNameList[0] + @" ON " + tableNameList[0] + @".idcpr = idContPers
                WHERE idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') 
                 AND idContPers NOT IN (SELECT DISTINCT idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a 
                ON ab.idArrangement = a.idArrangement WHERE (a.dtTOArrangement  Between (SELECT DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') 
                AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')) OR 
                (a.dtfromArrangement Between (SELECT  DATEADD(dd,-3,dtfromArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"') AND (SELECT  DATEADD(dd,+3,dttoArrangement) FROM Arrangement WHERE idArrangement = '" + idArrangement + @"')))
                AND idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') 
                AND idContPers IN (SELECT DISTINCT idContPers FROM ContactPersonAddress adr WHERE adr.idAddressType = '1' " + condition2 + ") " + condition1 +
                @" GROUP BY  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[0] + @".idMed)='" + newList.Count + "'");
            }

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetPersonBookedForArrangement(string idLang, int idContPers)
        {
            string query = string.Format(@"
               SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.nameStatus,pp.dtBooked,'Traveler' as type
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.nameStatus,ab.dtBooked FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus) pp ON pp.idContPers = cp.idContPers
                WHERE  cp.idContPers = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }


        // Nova metoda
        public DataTable GetTravelersReport(int idArrangement, int idContPers, string idLang, int gender, int status, int travelPapers, List<int> CheckedMedical, List<int> CheckedMedicalQuest)
        {
            List<string> tableNameList = new List<string>();

            List<string> tableLongNameList = new List<string>();

            if (CheckedMedical.Count > 0)
            {
                tableLongNameList.Add("MedCpr");
                tableNameList.Add("mcp");
            }

            string condition1 = "";
            string condition6 = "";
            string condition3 = "";
            string condition4 = "";
            string condition5 = "";

            List<int> newList = new List<int>();
            List<int> newListQuest = new List<int>();

            if (status > -1)
            {
                condition3 = " WHERE (ab.idStatus = '" + status + "')";
            }

            if (gender > -1)
            {
                if (status > -1)
                    condition4 = " AND (cp.idGender = '" + gender + "')";
                else
                    condition4 = " WHERE (cp.idGender = '" + gender + "')";

            }


            if (travelPapers > -1)
            {
                if (status > -1 || gender > -1)
                    condition5 = " AND (ab.idTravelPapers = '" + travelPapers + "')";
                else
                    condition5 = " WHERE (ab.idTravelPapers = '" + travelPapers + "')";


            }

            if (tableNameList.Count == 1)
            {

                newList = CheckedMedical;
                newListQuest = CheckedMedicalQuest;
                for (int i = 0; i < newList.Count; i++)
                {
                    if (i == 0)
                    {
                        condition1 = "AND (" + tableNameList[0] + ".idAns='" + CheckedMedical[i].ToString() + "' AND " + tableNameList[0] + ".idQuest ='" + CheckedMedicalQuest[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition1 = condition1 + " OR (" + tableNameList[0] + ".idAns='" + CheckedMedical[i].ToString() + "' AND " + tableNameList[0] + ".idQuest ='" + CheckedMedicalQuest[i].ToString() + "')";
                    }
                }

                if (newList.Count > 1)
                    condition1 = "AND (" + condition1.Substring(3, condition1.Length - 3) + ")";

                if (gender > -1 || status > -1 || travelPapers > -1)
                    condition1 = condition1;
                else
                    condition1 = "WHERE " + condition1.Substring(3, condition1.Length - 3);

            }


            string query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                INNER JOIN (SELECT DISTINCT idContPers,idStatus,idTravelPapers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') ab on cp.idContPers=ab.idContPers " + condition3 + condition4 + condition5);

            if (tableNameList.Count == 1)
            {
                query = string.Format(@"
                SELECT  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,count(" + tableNameList[0] + @".idMed)
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN " + tableLongNameList[0] + @" " + tableNameList[0] + @" ON " + tableNameList[0] + @".idcpr = idContPers
                INNER JOIN (SELECT DISTINCT idContPers, idStatus, idTravelPapers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"') ab on cp.idContPers=ab.idContPers"
                + condition3 + condition4 + condition5 + condition1 +
                @" GROUP BY  cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction
                HAVING count(" + tableNameList[0] + @".idMed)='" + newList.Count + "'");
            }


            return conn.executeSelectQuery(query, null);
        }

       
        public DataTable GetFemalePersonsVHForArrangement(int idArrangement, string idLang)
        {
            string query = string.Format(@"
                 SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.nameStatus
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.idStatus,abst.nameStatus FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus WHERE ab.idArrangement = '" + idArrangement + @"') pp ON pp.idContPers = cp.idContPers
                WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"')
                AND pp.idStatus <>'3' AND cp.idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')and cp.idGender = '2'");

            return conn.executeSelectQuery(query, null);
        }
        //======= Getting Reserve Vol Helpers 
        public DataTable GetReservesVHForArrangement(int idArrangement, string idLang)
        {
            string query = string.Format(@"
                 SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.nameStatus
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.idStatus,abst.nameStatus FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus WHERE ab.idArrangement = '" + idArrangement + @"') pp ON pp.idContPers = cp.idContPers
                WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"')
                AND pp.idStatus ='3' AND cp.idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }
        //=================================

        //======= Getting Reserve Travelers
        public DataTable GetReservesTravelerForArrangement(int idArrangement, string idLang)
        {
            string query = string.Format(@"
                 SELECT cp.idContPers,initialsContPers,firstname,midname,lastname,maidenname,cp.idTitle,t.nameTitle,cp.idGender,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender,birthdate,dtCreated,
                idUserCreated,dtModified,idUserModified,idUserResponsible,isMaried,isActive,isDied,dtOfDeath,isNeedProspect,
                isNeedMail,identBSN,isPayInvoice,cp.isSharePicture,isPaperByMail,isContactPerson,idClient,livesIn,idCpFunction,isRequestBrochure,pp.nameStatus
                FROM ContactPerson cp
                LEFT JOIN Gender g ON g.idGender = cp.idGender
                LEFT JOIN Title t ON t.idTitle = cp.idTitle
                LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey 
                LEFT OUTER JOIN (SELECT DISTINCT idContPers,abst.idStatus,abst.nameStatus FROM ArrangementBook ab LEFT OUTER JOIN ArrangementBookStatus abst ON abst.idStatus = ab.idStatus WHERE ab.idArrangement = '" + idArrangement + @"') pp ON pp.idContPers = cp.idContPers
                WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangement = '" + idArrangement + @"')
                AND pp.idStatus ='3' AND cp.idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }
        //=================================
    }


}
