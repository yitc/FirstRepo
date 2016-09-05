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
    public class ArrangementDAO
    {
        private dbConnection conn;

        public ArrangementDAO()
        {
            conn = new dbConnection();
        }



        public DataTable GetAllArrangements()
        {

            string query = string.Format(@"SELECT  DISTINCT a.idArrangement,codeArrangement,nameArrangement, dtFromArrangement,dtToArrangement,nrOfNights,
                                        cityArrangement,countryArrangement,c.nameCountry as countryNameArrangement,typeArrangement,t.nameArrType as typeNameArrangement,
                                        nrTraveler, minNrTraveler, nrVoluntaryHelper,  a.idHotelService, h.nameHotelService,
                                        isWeb, nrMaleVoluntary, idAgeCategory, nrMaximumWheelchairs, whoseElectricWheelchairs,buSupportingArms,statusArrangement,(SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement) as booked,
                                        (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 1 and a.idArrangement=m.idArrangement) as optionalBooked,
										(nrTraveler - (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement)) as freePlaces, daysFirstPayment,daysLastPayment,percentFirstPayment,
                                        reservationCosts, codeProject
										 FROM Arrangement a 
                                        LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                        LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType
                                        LEFT OUTER JOIN HotelService h on h.idHotelService = a.idHotelService
                                        Left  join ArrangementBook m on a.idArrangement = m.idArrangement
                                        Left  join ArrangementBook s on a.idArrangement = s.idArrangement");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllArrangementsAccount(string bookyear)
        {

            string query = string.Format(@"SELECT  DISTINCT a.idArrangement,codeArrangement,nameArrangement, dtFromArrangement,dtToArrangement,nrOfNights,
                                        cityArrangement,countryArrangement,c.nameCountry as countryNameArrangement,typeArrangement,t.nameArrType as typeNameArrangement,
                                        nrTraveler, minNrTraveler, nrVoluntaryHelper,  a.idHotelService, h.nameHotelService,
                                        isWeb, nrMaleVoluntary, idAgeCategory, nrMaximumWheelchairs, whoseElectricWheelchairs,buSupportingArms,statusArrangement,(SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement) as booked,
                                        (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 1 and a.idArrangement=m.idArrangement) as optionalBooked,
										(nrTraveler - (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement)) as freePlaces, daysFirstPayment,daysLastPayment,percentFirstPayment,
                                        reservationCosts, codeProject
										 FROM Arrangement a 
                                        LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                        LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType
                                        LEFT OUTER JOIN HotelService h on h.idHotelService = a.idHotelService
                                        Left  join ArrangementBook m on a.idArrangement = m.idArrangement
                                        Left  join ArrangementBook s on a.idArrangement = s.idArrangement  WHERE YEAR(dtFromArrangement) >= '" + bookyear + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementCodeProject(string code)
        {
            string query = string.Format(@"SELECT idArrangement,codeArrangement,codeProject
                                        FROM Arrangement where codeProject = '" +code+"'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementHotelService()
        {
            string query = string.Format(@"SELECT idHotelService,nameHotelService
                                        FROM HotelService");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllArrangementsNotInActiveContracts(int idClient)
        {

            string query = string.Format(@"SELECT a.idArrangement,codeArrangement,nameArrangement, dtFromArrangement,dtToArrangement,
                                        cityArrangement,countryArrangement,c.nameCountry as countryNameArrangement,typeArrangement,t.nameArrType as typeNameArrangement,
                                        nrTraveler, minNrTraveler, nrVoluntaryHelper,  a.idHotelService, h.nameHotelService, isWeb,nrMaleVoluntary,
                                        idAgeCategory, nrMaximumWheelchairs, whoseElectricWheelchairs,
                                        buSupportingArms,statusArrangement 
                                        FROM Arrangement a
                                        LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                        LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType
                                        LEFT OUTER JOIN HotelService h on h.idHotelService = a.idHotelService
                                        WHERE a.idArrangement NOT IN ( SELECT DISTINCT idArrangement FROM PriceList pl
                                        WHERE pl.idClient = '" + idClient + "' AND pl.isActive= '1')");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIfArrangement(int idClient, int idArrangement,int idPriceList)
        {

            string query = string.Format(@"SELECT a.idArrangement,codeArrangement,nameArrangement, dtFromArrangement,dtToArrangement,
                                        cityArrangement,countryArrangement,c.nameCountry as countryNameArrangement,typeArrangement,t.nameArrType as typeNameArrangement,
                                        nrTraveler, minNrTraveler, nrVoluntaryHelper,  a.idHotelService, h.nameHotelService, isWeb,nrMaleVoluntary,
                                        idAgeCategory, nrMaximumWheelchairs, whoseElectricWheelchairs,
                                        buSupportingArms,statusArrangement  
                                        FROM Arrangement a
                                        LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                        LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType
                                        LEFT OUTER JOIN HotelService h on h.idHotelService = a.idHotelService
                                        WHERE a.idArrangement IN ( SELECT DISTINCT idArrangement FROM PriceList pl
                                        WHERE pl.idClient = '" + idClient + "' AND pl.isActive= '1' AND a.idArrangement = '" + idArrangement + "' and idPriceList<>'" + idPriceList + "')");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllArrangementsMainGrid(int idFilter, List<int> labels, string idLang)
        {
            string query = string.Format(@"SELECT DISTINCT a.idArrangement,codeArrangement,nameArrangement, dtFromArrangement,dtToArrangement,nrOfNights,
                                        cityArrangement,countryArrangement,c.nameCountry as countryNameArrangement,typeArrangement,t.nameArrType as typeNameArrangement,
                                        nrTraveler, minNrTraveler, nrVoluntaryHelper, a.idHotelService, h.nameHotelService,
                                        isWeb, nrMaleVoluntary, a.idAgeCategory, cat.descAgeCategory , nrMaximumWheelchairs, whoseElectricWheelchairs,
                                        buSupportingArms,statusArrangement,(SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement) as booked,
                                        (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 1 and a.idArrangement=m.idArrangement) as optionalBooked,
										(nrTraveler - (SELECT COUNT(m.idContPers)as booked FROM ArrangementBook m WHERE m.idStatus = 2 and a.idArrangement=m.idArrangement)) as freePlaces,
                                        CASE WHEN ac.price IS NULL THEN 0 ELSE ac.price END AS price,  daysFirstPayment,daysLastPayment,percentFirstPayment,
                                        reservationCosts,nrAnchorage,invoiceDescription,idClientInvoice,nameClient
										 FROM Arrangement a 
                                        LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                        LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType
                                        LEFT OUTER JOIN HotelService h on h.idHotelService = a.idHotelService
                                        Left  OUTER JOIN ArrangementBook m on a.idArrangement = m.idArrangement
                                        Left  OUTER JOIN ArrangementBook s on a.idArrangement = s.idArrangement
                                        Left  OUTER JOIN ArrangementCalculation ac on a.idArrangement = ac.idArrangement
                                        LEFT OUTER JOIN Client cc on cc.idClient = a.idClientInvoice
                                        Left  OUTER JOIN AgeCategory cat on a.idAgeCategory = cat.idAgeCategory"
                + @"INNER JOIN ArrangementFilter af ON a.idArrangement = af.idArrangement
                WHERE af.idFilter = @idFilter");


            if (idFilter == 0)
            {
                query = query.Replace("WHERE af.idFilter = @idFilter", "");
                query = query.Replace("INNER JOIN ArrangementFilter af ON a.idArrangement = af.idArrangement", "");

                if (labels.Count > 0)
                {
                    query += " INNER JOIN ArrangementLabel al ON a.idArrangement = al.idArrangement ";
                    query += "WHERE ";
                    query += "(";
                    int count = 0;
                    foreach (var idlabel in labels)
                    {


                        if (count == 0)
                            query += "al.idLabel = '" + idlabel.ToString() + "' ";

                        if (count > 0)
                            query += " OR al.idLabel = '" + idlabel.ToString() + "' ";


                        count++;
                    }
                    query += ")";
                }

                return conn.executeSelectQuery(query, null);
            }
            else
            {
                if (labels.Count > 0)
                {
                    query = query.Replace("WHERE af.idFilter = @idFilter", "");
                    query += " INNER JOIN ArrangementLabel al ON a.idArrangement = al.idArrangement ";
                    query += "WHERE ";
                    query += "af.idFilter = @idFilter AND ";
                    query += "(";

                    int count = 0;
                    foreach (var idlabel in labels)
                    {


                        if (count == 0)
                            query += "al.idLabel = '" + idlabel.ToString() + "' ";

                        if (count > 0)
                            query += " OR al.idLabel = '" + idlabel.ToString() + "' ";


                        count++;
                    }
                    query += ")";

                }

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@idFilter", SqlDbType.Int);
                sqlParameters[0].Value = idFilter;
                return conn.executeSelectQuery(query, sqlParameters);
            }




       // }




        }

        public DataTable GetArrangementByCode(string codeProject)
        {
            string query = string.Format(@"SELECT idArrangement,codeArrangement,nameArrangement, dtFromArrangement,dtToArrangement,
                                        cityArrangement,countryArrangement, typeArrangement, nrTraveler, minNrTraveler, nrVoluntaryHelper, 
                                        a.idHotelService, isWeb, nrMaleVoluntary,buSupportingArms,statusArrangement,
                                        idAgeCategory, nrMaximumWheelchairs, whoseElectricWheelchairs, daysFirstPayment,daysLastPayment,percentFirstPayment,
                                        reservationCosts, codeProject,idClientInvoice  
                                        FROM Arrangement a WHERE codeProject = @codeProject");




            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@codeProject", SqlDbType.NVarChar);
            sqlParameters[0].Value = codeProject;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        //Aleksa i Mitar {country name, arrangament type field werent filled with data}
        public DataTable GetArrangementById(int idArrangement)
        {
            string query = string.Format(@"SELECT a.*, c.nameCountry as countryName,t.nameArrType as typeNameArrangement
                                           FROM Arrangement a
                                           LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                           LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType 
                                           WHERE idArrangement = @idArrangement");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetArrangementByArrangementBook(int idArrangementBook)
        {
            string query = string.Format(@"SELECT a.*, c.nameCountry as countryName,t.nameArrType as typeNameArrangement                                            
                                           FROM Arrangement a 
                                           LEFT OUTER JOIN Country c on countryArrangement = c.idCountry
                                           LEFT OUTER JOIN ArrType t on typeArrangement = t.idArrType 
                                           WHERE idArrangement IN (SELECT DISTINCT idArrangement FROM ArrangementBook where idArrangementBook = @idArrangementBook)");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameters[0].Value = idArrangementBook;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        //Aleksa i Mitar

        public DataTable GetLabelsArrangement(int idArrangement)
        {

            string query = string.Format(@"SELECT idArrangement,idLabel
                FROM ArrangementLabel 
                WHERE idArrangement = @idArrangement");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public int Save(ArrangementModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Arrangement (codeArrangement,nameArrangement,dtFromArrangement,dtToArrangement, nrOfNights,
                            cityArrangement,countryArrangement,typeArrangement, nrTraveler, minNrTraveler, nrVoluntaryHelper, idHotelService, isWeb,nrMaleVoluntary,
                            idAgeCategory, nrMaximumWheelchairs, whoseElectricWheelchairs,buSupportingArms,statusArrangement,
                            daysFirstPayment,daysLastPayment,percentFirstPayment,reservationCosts,nrAnchorage,invoiceDescription, codeProject, idClientInvoice) 
                      VALUES(@codeArrangement,@nameArrangement,@dtFromArrangement,@dtToArrangement,@nrOfNights,
                             @cityArrangement,@countryArrangement,@typeArrangement, @nrTraveler, @minNrTraveler, @nrVoluntaryHelper, @idHotelService, @isWeb,@nrMaleVoluntary,
                             @idAgeCategory, @nrMaximumWheelchairs, @whoseElectricWheelchairs,@buSupportingArms,@statusArrangement,
                             @daysFirstPayment,@daysLastPayment,@percentFirstPayment,@reservationCosts,@nrAnchorage,@invoiceDescription, @codeProject, @idClientInvoice)
                      ;SELECT SCOPE_IDENTITY();");


            SqlParameter[] sqlParameter = new SqlParameter[27];
            sqlParameter[0] = new SqlParameter("@codeArrangement", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.codeArrangement;

            sqlParameter[1] = new SqlParameter("@nameArrangement", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.nameArrangement;

            sqlParameter[2] = new SqlParameter("@dtFromArrangement", SqlDbType.Date);
            sqlParameter[2].Value = model.dtFromArrangement;

            sqlParameter[3] = new SqlParameter("@dtToArrangement", SqlDbType.Date);
            sqlParameter[3].Value = model.dtToArrangement;

            sqlParameter[4] = new SqlParameter("@cityArrangement", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.cityArrangement;

            sqlParameter[5] = new SqlParameter("@countryArrangement", SqlDbType.Int);
            sqlParameter[5].Value = model.countryArrangement;

            sqlParameter[6] = new SqlParameter("@typeArrangement", SqlDbType.Int);
            sqlParameter[6].Value = model.typeArrangement;

            sqlParameter[7] = new SqlParameter("@nrTraveler", SqlDbType.Int);
            sqlParameter[7].Value = model.nrTraveler;

            sqlParameter[8] = new SqlParameter("@minNrTraveler", SqlDbType.Int);
            sqlParameter[8].Value = model.minNrTraveler;

            sqlParameter[9] = new SqlParameter("@nrVoluntaryHelper", SqlDbType.Int);
            sqlParameter[9].Value = model.nrVoluntaryHelper;

            sqlParameter[10] = new SqlParameter("@idHotelService", SqlDbType.NVarChar);
            sqlParameter[10].Value = model.idHotelService;

            sqlParameter[11] = new SqlParameter("@isWeb", SqlDbType.Bit);
            sqlParameter[11].Value = model.isWeb;

            sqlParameter[12] = new SqlParameter("@nrMaleVoluntary", SqlDbType.Int);
            sqlParameter[12].Value = model.nrMaleVoluntary;

            sqlParameter[13] = new SqlParameter("@idAgeCategory", SqlDbType.Int);
            sqlParameter[13].Value = model.idAgeCategory;

            sqlParameter[14] = new SqlParameter("@nrMaximumWheelchairs", SqlDbType.Int);
            sqlParameter[14].Value = model.nrMaximumWheelchairs;

            sqlParameter[15] = new SqlParameter("@whoseElectricWheelchairs", SqlDbType.Int);
            sqlParameter[15].Value = model.whoseElectricWheelchairs;

            sqlParameter[16] = new SqlParameter("@buSupportingArms", SqlDbType.Int);
            sqlParameter[16].Value = model.buSupportingArms;

            sqlParameter[17] = new SqlParameter("@statusArrangement", SqlDbType.NVarChar);
            sqlParameter[17].Value = model.statusArrangement;

            sqlParameter[18] = new SqlParameter("@nrOfNights", SqlDbType.NVarChar);
            sqlParameter[18].Value = model.nrOfNights;

            sqlParameter[19] = new SqlParameter("@daysFirstPayment", SqlDbType.Int);
            sqlParameter[19].Value = model.daysFirstPayment;

            sqlParameter[20] = new SqlParameter("@daysLastPayment", SqlDbType.Int);
            sqlParameter[20].Value = model.daysLastPayment;

            sqlParameter[21] = new SqlParameter("@percentFirstPayment", SqlDbType.Decimal);
            sqlParameter[21].Value = model.percentFirstPayment;

            sqlParameter[22] = new SqlParameter("@reservationCosts", SqlDbType.Decimal);
            sqlParameter[22].Value = model.reservationCosts;

            sqlParameter[23] = new SqlParameter("@nrAnchorage", SqlDbType.Int);
            sqlParameter[23].Value = model.nrAnchorage;

            sqlParameter[24] = new SqlParameter("@invoiceDescription", SqlDbType.NVarChar);
            sqlParameter[24].Value = model.invoiceDescription;

            sqlParameter[25] = new SqlParameter("@codeProject", SqlDbType.NVarChar);
            sqlParameter[25].Value = model.codeProject;

            sqlParameter[26] = new SqlParameter("@idClientInvoice", SqlDbType.Int);
            sqlParameter[26].Value = model.idClientInvoice;

            sqlParameters.Add(sqlParameter);
            _query.Add(query);

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
            sqlParameter[4].Value = conn.GetLastTableID("Arrangement")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Arrangement";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);
        }

        public Boolean Update(ArrangementModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Arrangement SET codeArrangement=@codeArrangement,nameArrangement=@nameArrangement,
                            dtFromArrangement=@dtFromArrangement,dtToArrangement=@dtToArrangement,nrOfNights= @nrOfNights,cityArrangement=@cityArrangement,
                            countryArrangement=@countryArrangement,typeArrangement=@typeArrangement, 
                            nrTraveler=@nrTraveler, minNrTraveler=@minNrTraveler, nrVoluntaryHelper=@nrVoluntaryHelper,idHotelService=@idHotelService,
                            isWeb=@isWeb, nrMaleVoluntary=@nrMaleVoluntary,
                            idAgeCategory = @idAgeCategory, nrMaximumWheelchairs = @nrMaximumWheelchairs, whoseElectricWheelchairs = @whoseElectricWheelchairs,
                            buSupportingArms=@buSupportingArms, statusArrangement=@statusArrangement,daysFirstPayment=@daysFirstPayment,
                            daysLastPayment=@daysLastPayment,percentFirstPayment=@percentFirstPayment,reservationCosts=@reservationCosts,nrAnchorage=@nrAnchorage,invoiceDescription=@invoiceDescription, codeProject=@codeProject,idClientInvoice=@idClientInvoice
                            WHERE  idArrangement=@idArrangement");


            SqlParameter[] sqlParameter = new SqlParameter[28];
            sqlParameter[0] = new SqlParameter("@codeArrangement", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.codeArrangement;

            sqlParameter[1] = new SqlParameter("@nameArrangement", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.nameArrangement;

            sqlParameter[2] = new SqlParameter("@dtFromArrangement", SqlDbType.Date);
            sqlParameter[2].Value = model.dtFromArrangement;

            sqlParameter[3] = new SqlParameter("@dtToArrangement", SqlDbType.Date);
            sqlParameter[3].Value = model.dtToArrangement;

            sqlParameter[4] = new SqlParameter("@cityArrangement", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.cityArrangement;

            sqlParameter[5] = new SqlParameter("@countryArrangement", SqlDbType.Int);
            sqlParameter[5].Value = model.countryArrangement;

            sqlParameter[6] = new SqlParameter("@typeArrangement", SqlDbType.Int);
            sqlParameter[6].Value = model.typeArrangement;

            sqlParameter[7] = new SqlParameter("@nrTraveler", SqlDbType.Int);
            sqlParameter[7].Value = model.nrTraveler;

            sqlParameter[8] = new SqlParameter("@minNrTraveler", SqlDbType.Int);
            sqlParameter[8].Value = model.minNrTraveler;

            sqlParameter[9] = new SqlParameter("@nrVoluntaryHelper", SqlDbType.Int);
            sqlParameter[9].Value = model.nrVoluntaryHelper;

            sqlParameter[10] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[10].Value = model.idArrangement;

            sqlParameter[11] = new SqlParameter("@idHotelService", SqlDbType.NVarChar);
            sqlParameter[11].Value = model.idHotelService;

            sqlParameter[12] = new SqlParameter("@isWeb", SqlDbType.Bit);
            sqlParameter[12].Value = model.isWeb;

            sqlParameter[13] = new SqlParameter("@nrMaleVoluntary", SqlDbType.Int);
            sqlParameter[13].Value = model.nrMaleVoluntary;

            sqlParameter[14] = new SqlParameter("@idAgeCategory", SqlDbType.Int);
            sqlParameter[14].Value = model.idAgeCategory;

            sqlParameter[15] = new SqlParameter("@nrMaximumWheelchairs", SqlDbType.Int);
            sqlParameter[15].Value = model.nrMaximumWheelchairs;

            sqlParameter[16] = new SqlParameter("@whoseElectricWheelchairs", SqlDbType.Int);
            sqlParameter[16].Value = model.whoseElectricWheelchairs;

            sqlParameter[17] = new SqlParameter("@buSupportingArms", SqlDbType.Int);
            sqlParameter[17].Value = model.buSupportingArms;

            sqlParameter[18] = new SqlParameter("@statusArrangement", SqlDbType.NVarChar);
            sqlParameter[18].Value = model.statusArrangement;

            sqlParameter[19] = new SqlParameter("@nrOfNights", SqlDbType.NVarChar);
            sqlParameter[19].Value = model.nrOfNights;

            sqlParameter[20] = new SqlParameter("@daysFirstPayment", SqlDbType.Int);
            sqlParameter[20].Value = model.daysFirstPayment;

            sqlParameter[21] = new SqlParameter("@daysLastPayment", SqlDbType.Int);
            sqlParameter[21].Value = model.daysLastPayment;

            sqlParameter[22] = new SqlParameter("@percentFirstPayment", SqlDbType.Decimal);
            sqlParameter[22].Value = model.percentFirstPayment;

            sqlParameter[23] = new SqlParameter("@reservationCosts", SqlDbType.Decimal);
            sqlParameter[23].Value = model.reservationCosts;

            sqlParameter[24] = new SqlParameter("@nrAnchorage", SqlDbType.Int);
            sqlParameter[24].Value = model.nrAnchorage;

            sqlParameter[25] = new SqlParameter("@invoiceDescription", SqlDbType.NVarChar);
            sqlParameter[25].Value = model.invoiceDescription;

            sqlParameter[26] = new SqlParameter("@codeProject", SqlDbType.NVarChar);
            sqlParameter[26].Value = model.codeProject;

            sqlParameter[27] = new SqlParameter("@idClientInvoice", SqlDbType.Int);
            sqlParameter[27].Value = model.idClientInvoice;
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
            sqlParameter[4].Value = model.idArrangement;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Arrangement";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idArrangement, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Arrangement WHERE idArrangement = @idArrangement ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementCalculation WHERE idArrangement = @idArrangement ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementCalculationFirstArticles WHERE idArrangement = @idArrangement ");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementCalculationFirstNotArticles WHERE idArrangement = @idArrangement ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementCalculationSecond WHERE idArrangement = @idArrangement ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementFilter WHERE idArrangement = @idArrangement ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementLabel WHERE idArrangement = @idArrangement ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementLabelFirst WHERE idArrangement = @idArrangement ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementTargetGroup WHERE idArrangement = @idArrangement ");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementThemeTrip WHERE idArrangement = @idArrangement");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"UPDATE Contacts SET idArrangement = '0' WHERE idArrangement = @idArrangement");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"UPDATE Documents SET idArrangement = '0' WHERE idArrangement = @idArrangement");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"UPDATE ToDo SET idArrangement = '0' WHERE idArrangement = @idArrangement");

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolTripArr WHERE idArrangement = @idArrangement");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolFunctionArr WHERE idArrangement = @idArrangement");


            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM VolTripArr WHERE idArrangement = @idArrangement");


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
            sqlParameter[4].Value = idArrangement;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Arrangement";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable IsIn(int idArrangement)
        {
            string query = string.Format(@"SELECT pl.idArrangement  AS idArrangement
                                       FROM PriceList pl
                                       WHERE pl.idArrangement = @idArrangement
                                       UNION 
                                       SELECT ab.idArrangement AS idArrangement
                                       FROM ArrangementBook ab
                                       WHERE ab.idArrangement=@idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GeArrangementsLookup()
        {

            string query = string.Format(@"SELECT  DISTINCT idArrangement , statusArrangement, codeArrangement , nameArrangement, dtFromArrangement,dtToArrangement, nrOfNights,cityArrangement ,
                   countryArrangement ,c.nameCountry as countryNameArrangement, typeArrangement, at.nameArrType as typeNameArrangement ,nrTraveler , minNrTraveler ,nrVoluntaryHelper ,
                   a.idHotelService , hs.nameHotelService , isWeb , nrMaleVoluntary,a.idAgeCategory, ac.descAgeCategory ,nrMaximumWheelchairs, whoseElectricWheelchairs ,
                   buSupportingArms, buRollators, daysFirstPayment , daysLastPayment, percentFirstPayment ,
                   reservationCosts , nrAnchorage
			       FROM Arrangement a
			       LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
			       LEFT OUTER JOIN HotelService hs on a.idHotelService= hs.idHotelService
			       LEFT OUTER JOIN AgeCategory ac on a.idAgeCategory=ac.idAgeCategory
			       LEFT OUTER JOIN ArrType at on a.typeArrangement= at.idArrType 
                   ORDER BY idArrangement");

            return conn.executeSelectQuery(query, null);
        }
    }


}