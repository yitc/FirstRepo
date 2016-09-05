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
    public class ArrangementCalculationDAO
    {
        private dbConnection conn;

        public ArrangementCalculationDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetArrangementCalculation(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement, provision, calamiteitenFonds, correction, travelInsurace, travelInsurance2, 
                polisCosts, price, moneyGroup,insuranceVolontary,singleRoomPrice,discount,txt,txtAmount,numberLeader, premie1,premie2,numberCO,minNumberTravelers,
                volontaryDays, isSport,nrVoluntary
                FROM ArrangementCalculation WHERE idArrangement = @idArrangement");
        
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetArrangementCalculationSecond(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement, provision, calamiteitenFonds, correction, travelInsurace, travelInsurance2, 
                polisCosts, price, moneyGroup,insuranceVolontary,singleRoomPrice,discount,txt,txtAmount,numberLeader, premie1,premie2,numberCO,minNumberTravelers,
                volontaryDays, isSport,nrTraveler,nrVoluntaryHelper,nrVoluntary
                FROM ArrangementCalculationSecond WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable isCalculationFinished(int idArrangement)
        {
            string query = string.Format(@"SELECT isFinished FROM ArrangementCalculation WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(ArrangementCalculationModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            
            string query = string.Format(@"SELECT idArrangement FROM ArrangementCalculation WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = model.idArrangement;


            if (conn.executeSelectQuery(query, sqlParameter).Rows.Count <= 0)
            {

                query = string.Format(@"INSERT INTO ArrangementCalculation (idArrangement, minNumberTravelers, provision, calamiteitenFonds, correction, travelInsurace, travelInsurance2, 
                        polisCosts, price, moneyGroup, insuranceVolontary,singleRoomPrice,discount,txt,txtAmount,numberLeader, premie1,premie2,numberCO,
                        volontaryDays, isSport,nrVoluntary) 
                      VALUES(@idArrangement, @minNumberTravelers, @provision, @calamiteitenFonds, @correction, @travelInsurace, @travelInsurance2, 
                        @polisCosts, @price, @moneyGroup, @insuranceVolontary, @singleRoomPrice,@discount,@txt,@txtAmount,@numberLeader, @premie1,@premie2,@numberCO,
                        @volontaryDays, @isSport,@nrVoluntary)");

                sqlParameter = new SqlParameter[22];

                sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                sqlParameter[0].Value = model.idArrangement;

                sqlParameter[1] = new SqlParameter("@minNumberTravelers", SqlDbType.Int);
                sqlParameter[1].Value = model.minNumberTravelers;

                sqlParameter[2] = new SqlParameter("@provision", SqlDbType.Decimal);
                sqlParameter[2].Value = model.provision;

                sqlParameter[3] = new SqlParameter("@calamiteitenFonds", SqlDbType.Decimal);
                sqlParameter[3].Value = model.calamiteitenFonds;

                sqlParameter[4] = new SqlParameter("@correction", SqlDbType.Decimal);
                sqlParameter[4].Value = model.correction;

                sqlParameter[5] = new SqlParameter("@travelInsurace", SqlDbType.Decimal);
                sqlParameter[5].Value = model.travelInsurace;

                sqlParameter[6] = new SqlParameter("@travelInsurance2", SqlDbType.Decimal);
                sqlParameter[6].Value = model.travelInsurance2;

                sqlParameter[7] = new SqlParameter("@polisCosts", SqlDbType.Decimal);
                sqlParameter[7].Value = model.polisCosts;

                sqlParameter[8] = new SqlParameter("@price", SqlDbType.Decimal);
                sqlParameter[8].Value = model.price;

                sqlParameter[9] = new SqlParameter("@moneyGroup", SqlDbType.Decimal);
                sqlParameter[9].Value = model.moneyGroup;

                sqlParameter[10] = new SqlParameter("@insuranceVolontary", SqlDbType.Decimal);
                sqlParameter[10].Value = model.insuranceVolontary;

                sqlParameter[11] = new SqlParameter("@singleRoomPrice", SqlDbType.Decimal);
                sqlParameter[11].Value = model.singleRoomPrice;

                sqlParameter[12] = new SqlParameter("@discount", SqlDbType.Decimal);
                sqlParameter[12].Value = model.discount;

                sqlParameter[13] = new SqlParameter("@txt", SqlDbType.NVarChar);
                sqlParameter[13].Value = model.txt;

                sqlParameter[14] = new SqlParameter("@txtAmount", SqlDbType.Decimal);
                sqlParameter[14].Value = model.txtAmount;


                sqlParameter[15] = new SqlParameter("@numberLeader", SqlDbType.Int);
                sqlParameter[15].Value = model.numberLeader;

                sqlParameter[16] = new SqlParameter("@premie1", SqlDbType.Decimal);
                sqlParameter[16].Value = model.premie1;

                sqlParameter[17] = new SqlParameter("@premie2", SqlDbType.Decimal);
                sqlParameter[17].Value = model.premie2;

                sqlParameter[18] = new SqlParameter("@numberCO", SqlDbType.Int);
                sqlParameter[18].Value = model.numberCO;

                sqlParameter[19] = new SqlParameter("@volontaryDays", SqlDbType.Int);
                sqlParameter[19].Value = model.volontaryDays;

                sqlParameter[20] = new SqlParameter("@isSport", SqlDbType.Bit);
                sqlParameter[20].Value = model.isSport;

                sqlParameter[21] = new SqlParameter("@nrVoluntary", SqlDbType.Int);
                sqlParameter[21].Value = model.nrVoluntary;




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
                sqlParameter[4].Value = model.idArrangement;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "codeCost";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ArrangementCalculation";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Save";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
            {
                query = string.Format(@"UPDATE ArrangementCalculation SET idArrangement=@idArrangement, minNumberTravelers=@minNumberTravelers, provision=@provision, 
                    calamiteitenFonds=@calamiteitenFonds, correction=@correction, travelInsurace=@travelInsurace, travelInsurance2=@travelInsurance2, 
                    polisCosts=@polisCosts, price=@price, moneyGroup=@moneyGroup,insuranceVolontary=@insuranceVolontary,singleRoomPrice=@singleRoomPrice, discount = @discount,
                    txt = @txt, txtAmount = @txtAmount,numberLeader=@numberLeader, premie1=@premie1,premie2=@premie2,numberCO=@numberCO,
                    volontaryDays=@volontaryDays, isSport=@isSport,nrVoluntary=@nrVoluntary
                   WHERE idArrangement = @idArrangement");

                sqlParameter = new SqlParameter[22];

                sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                sqlParameter[0].Value = model.idArrangement;

                sqlParameter[1] = new SqlParameter("@minNumberTravelers", SqlDbType.Int);
                sqlParameter[1].Value = model.minNumberTravelers;

                sqlParameter[2] = new SqlParameter("@provision", SqlDbType.Decimal);
                sqlParameter[2].Value = model.provision;

                sqlParameter[3] = new SqlParameter("@calamiteitenFonds", SqlDbType.Decimal);
                sqlParameter[3].Value = model.calamiteitenFonds;

                sqlParameter[4] = new SqlParameter("@correction", SqlDbType.Decimal);
                sqlParameter[4].Value = model.correction;

                sqlParameter[5] = new SqlParameter("@travelInsurace", SqlDbType.Decimal);
                sqlParameter[5].Value = model.travelInsurace;

                sqlParameter[6] = new SqlParameter("@travelInsurance2", SqlDbType.Decimal);
                sqlParameter[6].Value = model.travelInsurance2;

                sqlParameter[7] = new SqlParameter("@polisCosts", SqlDbType.Decimal);
                sqlParameter[7].Value = model.polisCosts;

                sqlParameter[8] = new SqlParameter("@price", SqlDbType.Decimal);
                sqlParameter[8].Value = model.price;

                sqlParameter[9] = new SqlParameter("@moneyGroup", SqlDbType.Decimal);
                sqlParameter[9].Value = model.moneyGroup;

                sqlParameter[10] = new SqlParameter("@insuranceVolontary", SqlDbType.Decimal);
                sqlParameter[10].Value = model.insuranceVolontary;

                sqlParameter[11] = new SqlParameter("@singleRoomPrice", SqlDbType.Decimal);
                sqlParameter[11].Value = model.singleRoomPrice;

                sqlParameter[12] = new SqlParameter("@discount", SqlDbType.Decimal);
                sqlParameter[12].Value = model.discount;

                sqlParameter[13] = new SqlParameter("@txt", SqlDbType.NVarChar);
                sqlParameter[13].Value = model.txt;

                sqlParameter[14] = new SqlParameter("@txtAmount", SqlDbType.Decimal);
                sqlParameter[14].Value = model.txtAmount;

                sqlParameter[15] = new SqlParameter("@numberLeader", SqlDbType.Int);
                sqlParameter[15].Value = model.numberLeader;

                sqlParameter[16] = new SqlParameter("@premie1", SqlDbType.Decimal);
                sqlParameter[16].Value = model.premie1;

                sqlParameter[17] = new SqlParameter("@premie2", SqlDbType.Decimal);
                sqlParameter[17].Value = model.premie2;

                sqlParameter[18] = new SqlParameter("@numberCO", SqlDbType.Int);
                sqlParameter[18].Value = model.numberCO;

                sqlParameter[19] = new SqlParameter("@volontaryDays", SqlDbType.Int);
                sqlParameter[19].Value = model.volontaryDays;

                sqlParameter[20] = new SqlParameter("@isSport", SqlDbType.Bit);
                sqlParameter[20].Value = model.isSport;

                sqlParameter[21] = new SqlParameter("@nrVoluntary", SqlDbType.Int);
                sqlParameter[21].Value = model.nrVoluntary;



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
                sqlParameter[4].Value = model.idArrangement;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idArrangement";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ArrangementCalculation";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Save";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);

                return conn.executQueryTransaction(_query, sqlParameters);
            }
        }

        public Boolean SaveSecond(ArrangementCalculationSecondModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"SELECT idArrangement FROM ArrangementCalculationSecond WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = model.idArrangement;

            if (conn.executeSelectQuery(query, sqlParameter).Rows.Count <= 0)
            {

                query = string.Format(@"INSERT INTO ArrangementCalculationSecond (idArrangement, minNumberTravelers, provision, calamiteitenFonds, correction, travelInsurace, travelInsurance2, 
                        polisCosts, price, moneyGroup, insuranceVolontary,singleRoomPrice,discount,txt,txtAmount,numberLeader, premie1,premie2,numberCO,
                        volontaryDays, isSport,nrTraveler,nrVoluntaryHelper,nrVoluntary) 
                      VALUES(@idArrangement, @minNumberTravelers, @provision, @calamiteitenFonds, @correction, @travelInsurace, @travelInsurance2, 
                        @polisCosts, @price, @moneyGroup, @insuranceVolontary, @singleRoomPrice,@discount,@txt,@txtAmount,@numberLeader, @premie1,@premie2,@numberCO,
                        @volontaryDays, @isSport,@nrTraveler,@nrVoluntaryHelper,@nrVoluntary)");

                sqlParameter = new SqlParameter[24];

                sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                sqlParameter[0].Value = model.idArrangement;

                sqlParameter[1] = new SqlParameter("@minNumberTravelers", SqlDbType.Int);
                sqlParameter[1].Value = model.minNumberTravelers;

                sqlParameter[2] = new SqlParameter("@provision", SqlDbType.Decimal);
                sqlParameter[2].Value = model.provision;

                sqlParameter[3] = new SqlParameter("@calamiteitenFonds", SqlDbType.Decimal);
                sqlParameter[3].Value = model.calamiteitenFonds;

                sqlParameter[4] = new SqlParameter("@correction", SqlDbType.Decimal);
                sqlParameter[4].Value = model.correction;

                sqlParameter[5] = new SqlParameter("@travelInsurace", SqlDbType.Decimal);
                sqlParameter[5].Value = model.travelInsurace;

                sqlParameter[6] = new SqlParameter("@travelInsurance2", SqlDbType.Decimal);
                sqlParameter[6].Value = model.travelInsurance2;

                sqlParameter[7] = new SqlParameter("@polisCosts", SqlDbType.Decimal);
                sqlParameter[7].Value = model.polisCosts;

                sqlParameter[8] = new SqlParameter("@price", SqlDbType.Decimal);
                sqlParameter[8].Value = model.price;

                sqlParameter[9] = new SqlParameter("@moneyGroup", SqlDbType.Decimal);
                sqlParameter[9].Value = model.moneyGroup;

                sqlParameter[10] = new SqlParameter("@insuranceVolontary", SqlDbType.Decimal);
                sqlParameter[10].Value = model.insuranceVolontary;

                sqlParameter[11] = new SqlParameter("@singleRoomPrice", SqlDbType.Decimal);
                sqlParameter[11].Value = model.singleRoomPrice;

                sqlParameter[12] = new SqlParameter("@discount", SqlDbType.Decimal);
                sqlParameter[12].Value = model.discount;

                sqlParameter[13] = new SqlParameter("@txt", SqlDbType.NVarChar);
                sqlParameter[13].Value = model.txt;

                sqlParameter[14] = new SqlParameter("@txtAmount", SqlDbType.Decimal);
                sqlParameter[14].Value = model.txtAmount;

                sqlParameter[15] = new SqlParameter("@numberLeader", SqlDbType.Int);
                sqlParameter[15].Value = model.numberLeader;

                sqlParameter[16] = new SqlParameter("@premie1", SqlDbType.Decimal);
                sqlParameter[16].Value = model.premie1;

                sqlParameter[17] = new SqlParameter("@premie2", SqlDbType.Decimal);
                sqlParameter[17].Value = model.premie2;

                sqlParameter[18] = new SqlParameter("@numberCO", SqlDbType.Int);
                sqlParameter[18].Value = model.numberCO;

                sqlParameter[19] = new SqlParameter("@volontaryDays", SqlDbType.Int);
                sqlParameter[19].Value = model.volontaryDays;

                sqlParameter[20] = new SqlParameter("@isSport", SqlDbType.Bit);
                sqlParameter[20].Value = model.isSport;

                sqlParameter[21] = new SqlParameter("@nrTraveler", SqlDbType.Int);
                sqlParameter[21].Value = model.nrTraveler;

                sqlParameter[22] = new SqlParameter("@nrVoluntaryHelper", SqlDbType.Int);
                sqlParameter[22].Value = model.nrVoluntaryHelper;

                sqlParameter[23] = new SqlParameter("@nrVoluntary", SqlDbType.Int);
                sqlParameter[23].Value = model.nrVoluntary;

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
                sqlParameter[4].Value = model.idArrangement;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idArrangement";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ArrangementCalculationSecond";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Save second";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);

            }
            else
            {
                query = string.Format(@"UPDATE ArrangementCalculationSecond SET idArrangement=@idArrangement, minNumberTravelers=@minNumberTravelers, provision=@provision, 
                    calamiteitenFonds=@calamiteitenFonds, correction=@correction, travelInsurace=@travelInsurace, travelInsurance2=@travelInsurance2, 
                    polisCosts=@polisCosts, price=@price, moneyGroup=@moneyGroup,insuranceVolontary=@insuranceVolontary,singleRoomPrice=@singleRoomPrice, discount = @discount,
                    txt = @txt, txtAmount = @txtAmount,numberLeader=@numberLeader, premie1=@premie1,premie2=@premie2,numberCO=@numberCO,
                    volontaryDays=@volontaryDays, isSport=@isSport,nrTraveler=@nrTraveler,nrVoluntaryHelper=@nrVoluntaryHelper, nrVoluntary=@nrVoluntary
                    WHERE idArrangement = @idArrangement");

                sqlParameter = new SqlParameter[24];

                sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                sqlParameter[0].Value = model.idArrangement;

                sqlParameter[1] = new SqlParameter("@minNumberTravelers", SqlDbType.Int);
                sqlParameter[1].Value = model.minNumberTravelers;

                sqlParameter[2] = new SqlParameter("@provision", SqlDbType.Decimal);
                sqlParameter[2].Value = model.provision;

                sqlParameter[3] = new SqlParameter("@calamiteitenFonds", SqlDbType.Decimal);
                sqlParameter[3].Value = model.calamiteitenFonds;

                sqlParameter[4] = new SqlParameter("@correction", SqlDbType.Decimal);
                sqlParameter[4].Value = model.correction;

                sqlParameter[5] = new SqlParameter("@travelInsurace", SqlDbType.Decimal);
                sqlParameter[5].Value = model.travelInsurace;

                sqlParameter[6] = new SqlParameter("@travelInsurance2", SqlDbType.Decimal);
                sqlParameter[6].Value = model.travelInsurance2;

                sqlParameter[7] = new SqlParameter("@polisCosts", SqlDbType.Decimal);
                sqlParameter[7].Value = model.polisCosts;

                sqlParameter[8] = new SqlParameter("@price", SqlDbType.Decimal);
                sqlParameter[8].Value = model.price;

                sqlParameter[9] = new SqlParameter("@moneyGroup", SqlDbType.Decimal);
                sqlParameter[9].Value = model.moneyGroup;

                sqlParameter[10] = new SqlParameter("@insuranceVolontary", SqlDbType.Decimal);
                sqlParameter[10].Value = model.insuranceVolontary;

                sqlParameter[11] = new SqlParameter("@singleRoomPrice", SqlDbType.Decimal);
                sqlParameter[11].Value = model.singleRoomPrice;

                sqlParameter[12] = new SqlParameter("@discount", SqlDbType.Decimal);
                sqlParameter[12].Value = model.discount;

                sqlParameter[13] = new SqlParameter("@txt", SqlDbType.NVarChar);
                sqlParameter[13].Value = model.txt;

                sqlParameter[14] = new SqlParameter("@txtAmount", SqlDbType.Decimal);
                sqlParameter[14].Value = model.txtAmount;

                sqlParameter[15] = new SqlParameter("@numberLeader", SqlDbType.Int);
                sqlParameter[15].Value = model.numberLeader;

                sqlParameter[16] = new SqlParameter("@premie1", SqlDbType.Decimal);
                sqlParameter[16].Value = model.premie1;

                sqlParameter[17] = new SqlParameter("@premie2", SqlDbType.Decimal);
                sqlParameter[17].Value = model.premie2;

                sqlParameter[18] = new SqlParameter("@numberCO", SqlDbType.Int);
                sqlParameter[18].Value = model.numberCO;

                sqlParameter[19] = new SqlParameter("@volontaryDays", SqlDbType.Int);
                sqlParameter[19].Value = model.volontaryDays;

                sqlParameter[20] = new SqlParameter("@isSport", SqlDbType.Bit);
                sqlParameter[20].Value = model.isSport;

                sqlParameter[21] = new SqlParameter("@nrTraveler", SqlDbType.Int);
                sqlParameter[21].Value = model.nrTraveler;

                sqlParameter[22] = new SqlParameter("@nrVoluntaryHelper", SqlDbType.Int);
                sqlParameter[22].Value = model.nrVoluntaryHelper;

                sqlParameter[23] = new SqlParameter("@nrVoluntary", SqlDbType.Int);
                sqlParameter[23].Value = model.nrVoluntary;

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
                sqlParameter[4].Value = model.idArrangement;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idArrangement";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ArrangementCalculationSecond";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Save second";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);

                return conn.executQueryTransaction(_query, sqlParameters);

            }
        }

        public Boolean SaveFinished(int idArrangement, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementCalculation SET isFinished='true'
                   WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

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
            sqlParameter[4].Value = idArrangement;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementCalculation";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save finished";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable GetACalculationRepotr(DateTime dateFrom, DateTime dateTo, int idLabel)
        {
            string lcwhere = "";
            if (idLabel == 0)
                lcwhere = " WHERE  a.dtFromArrangement>='" + dateFrom.ToString("MM/dd/yyyy") + "' and dtToArrangement<='" + dateTo.ToString("MM/dd/yyyy") + "' order by dtFromArrangement";
            else
                lcwhere = " WHERE l.idLab='" + idLabel + "' AND a.dtFromArrangement>='" + dateFrom.ToString("MM/dd/yyyy") + "' and dtToArrangement<='" + dateTo.ToString("MM/dd/yyyy") + "' order by dtFromArrangement";

            string query = string.Format(@"SELECT  DISTINCT a.idArrangement,codeArrangement,a.dtFromArrangement,
            CASE WHEN ac.price is not null and a.minNrTraveler is not null THEN ac.price * a.minNrTraveler ELSE 0 END as total,
            CASE WHEN acfa.sumPriceTotalArrCFirst IS NOT NULL THEN acfa.sumPriceTotalArrCFirst ELSE 0 END as subTotal,
            CASE WHEN acs.price IS NOT NULL AND abbook.num IS NOT NULL THEN acs.price*abbook.num ELSE 0 END as totalSecond,
            CASE WHEN pla.sumPriceTotalPLArticles IS not null  THEN pla.sumPriceTotalPLArticles  ELSE  0 END  subTotalSecondPla,
            CASE WHEN ap.sumPriceTotalArrPrice IS not null  THEN ap.sumPriceTotalArrPrice  ELSE  0 END  subTotalSecondAp,
            CASE WHEN acCredit.credit IS NOT NULL THEN acCredit.credit ELSE 0 END as total2,
            CASE WHEN acDebit.debit IS NOT NULL THEN acDebit.debit ELSE 0 END as subTotal2

              FROM Arrangement a 
              LEFT OUTER JOIN ArrangementCalculation ac on a.idArrangement=ac.idArrangement
              LEFT OUTER JOIN (select SUM(CASE WHEN art.isGroup='true'  THEN priceTotal ELSE acf.priceTotal*acf.nrArticle*art.quantity END) as sumPriceTotalArrCFirst, idArrangement 
                         FROM  ArrangementCalculationFirstArticles acf 
                         LEFT OUTER JOIN Artical art on acf.idArticle=art.codeArtical  
                         WHERE isExtra='False'
                         GROUP BY idArrangement) acfa on a.idArrangement=acfa.idArrangement
              LEFT OUTER JOIN ArrangementCalculationSecond acs on a.idArrangement= acs.idArrangement
              LEFT OUTER JOIN PriceList pl on pl.idArrangement=a.idArrangement
              LEFT OUTER JOIN (SELECT SUM(CASE WHEN art.isGroup='true'  THEN pll.priceTotal ELSE pll.priceTotal*pll.nrArticle*art.quantity END)as sumPriceTotalPLArticles,pl.idArrangement
                         FROM  PriceList pl 
                         LEFT OUTER JOIN PriceListArticles pll on pll.idPricelist=pl.idPricelist AND isExtra='False'   
                         LEFT JOIN Artical art ON art.codeArtical = pll.idArticle  
                         GROUP BY pl.idArrangement )pla on  pla.idArrangement= a.idArrangement  
              LEFT OUTER JOIN ArrangementBook m on a.idArrangement = m.idArrangement
              LEFT OUTER JOIN (SELECT SUM(CASE WHEN art.isGroup='true'  THEN ap.priceTotal ELSE ap.priceTotal*ap.nrArticle*art.quantity END) as sumPriceTotalArrPrice, idArrangement 
                         FROM ArrangementPrice ap
                         LEFT JOIN Artical art ON art.codeArtical = ap.idArticle 
                         WHERE ap.isExtra='False'
                         GROUP BY ap.idArrangement) ap on ap.idArrangement= a.idArrangement 
              LEFT OUTER JOIN (select sum(debitLine) as debit, idProjectLine
                         FROM AccLine   
                         WHERE numberLedAccount>=4000 AND numberLedAccount<8000 and idProjectLine IS NOT NULL and idProjectLine <>''
                         GROUP by idProjectLine) acDebit on acDebit.idProjectLine =a.codeProject
              LEFT OUTER JOIN (select sum(creditLine) as credit, idProjectLine
                         FROM AccLine   
                         WHERE numberLedAccount>=8000 AND numberLedAccount<9000 and idProjectLine IS NOT NULL and idProjectLine <>''
                         GROUP by idProjectLine) acCredit on acCredit.idProjectLine=a.codeProject
              LEFT OUTER JOIN (SELECT COUNT (a.idContPers) as num, idArrangement
                         FROM ArrangementBook a
                         LEFT OUTER JOIN Invoice i on a.idArrangement=i.idVoucher
                         WHERE a.idStatus ='2' AND a.idContPers   IN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter != '4') 
                          AND a.idArrangementBook IN (SELECT idArrangementBook FROM Invoice )
                         GROUP by a.idArrangement) abbook ON  a.idArrangement=abbook.idArrangement
              LEFT OUTER JOIN ArrangementLabel al on al.idArrangement=a.idArrangement
              LEFT OUTER JOIN (SELECT CASE WHEN id is not null then id ELSE idLabel END as idLab FROM Labels) l on l.idLab=al.idLabel" + lcwhere);
 
          //     WHERE l.idLab='" + idLabel + "' AND a.dtFromArrangement>='" + dateFrom.ToString("MM/dd/yyyy") + "' and dtToArrangement<='" + dateTo.ToString("MM/dd/yyyy") + "' order by dtFromArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dateFrom;

            sqlParameters[1] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dateTo;

            sqlParameters[2] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[2].Value = idLabel;
            return conn.executeSelectQuery(query, sqlParameters);
        }

    }
}
