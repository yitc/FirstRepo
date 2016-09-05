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
    public class AccSettingsDAO
    {
        private dbConnection conn;
        public AccSettingsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllAccS()
        {

            string query = string.Format(@" SELECT * FROM AccSettings ");
           

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllAccSettings(string year)
        {
            
            string query = string.Format(@" SELECT idSettings, yearSettings, noPeriods, beginBookYear, endBookYear, isVat, defDebitorAccount, 
                                           defCreditorAccount, defVatDebitor, defVatCreditor, currDeferenceAccount, paymentDiferenceAccount,
                                           bankCostAccount, defPayCondition, noDayFrstWarning, noDaySecondWorning,defBTWinvoicing,defLedgerPrice,
                                            defLedgerIncurance, defLedgerCancel,defLedgerCalamitu,defLedgerMoneyGr,idDailyFak, labelSettings, defTransferingAcc,
                                            defReservationAcc,defLedgerCancelation,myIban, myBic, sepaPath,defFirstPayment,defLedReservationCost,debitorReservationAccount,
                                            defSepaAcc,userCreated,dtCreated,userModified,dtModified,defDifferenceAcc
                                           FROM AccSettings WHERE yearSettings = @year");
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@year", SqlDbType.NVarChar);
            sqlParameters[0].Value = year;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAccSettingsByID(string year)
        {
            string query = string.Format(@" SELECT idSettings, yearSettings, noPeriods, beginBookYear, endBookYear, isVat, defDebitorAccount, 
                                            defCreditorAccount, defVatDebitor, defVatCreditor, currDeferenceAccount, paymentDiferenceAccount,
                                            bankCostAccount, defPayCondition, noDayFrstWarning, noDaySecondWorning,defBTWinvoicing,defLedgerPrice,
                                            defLedgerIncurance, defLedgerCancel,defLedgerCalamitu,defLedgerMoneyGr,idDailyFak, labelSettings,defTransferingAcc,
                                             defReservationAcc,defLedgerCancelation,myIban, myBic, sepaPath,defFirstPayment,defLedReservationCost,debitorReservationAccount,
                                             defSepaAcc ,userCreated,dtCreated,userModified,dtModified,defDifferenceAcc
                                            FROM AccSettings
                                            WHERE yearSettings=@year  ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@year", SqlDbType.NVarChar);
            sqlParameters[0].Value = year;
           


            return conn.executeSelectQuery(query, sqlParameters);           
        }

        public Boolean ClientInvoice( string client, string invoice, string yearb)
        {
            bool empty = false;
            DataTable aa = new DataTable();
            string query = string.Format(@"SELECT * FROM AccLine WHERE idClientLine = '" + client + "' and invoiceNr = '" + invoice.Trim() + "' and bookingYear='"+yearb+"' ");
            aa=conn.executeSelectQuery(query, null);
            if (aa.Rows.Count == 0)
                empty = true;
            else
                empty = false;
            return empty;

        }

        public Boolean Save(AccSettingsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@" INSERT INTO AccSettings 
                                           (yearSettings, noPeriods, beginBookYear, endBookYear, isVat, defDebitorAccount, 
                                           defCreditorAccount, defVatDebitor, defVatCreditor, currDeferenceAccount, paymentDiferenceAccount,
                                           bankCostAccount, defPayCondition, noDayFrstWarning, noDaySecondWorning,defBTWinvoicing,defLedgerPrice,
                                            defLedgerIncurance, defLedgerCancel,defLedgerCalamitu,defLedgerMoneyGr,idDailyFak,labelSettings,defTransferingAcc,
                                             defReservationAcc,defLedgerCancelation,myIban, myBic, sepaPath,defFirstPayment,defLedReservationCost,debitorReservationAccount,
                                             defSepaAcc,userCreated,dtCreated,userModified,dtModified,defDifferenceAcc) 
                                           VALUES 
                                           ( @yearSettings, @noPeriods, @beginBookYear, @endBookYear, @isVat, @defDebitorAccount,
                                           @defCreditorAccount, @defVatDebitor, @defVatCreditor, @currDeferenceAccount, @paymentDiferenceAccount,
                                           @bankCostAccount, @defPayCondition, @noDayFrstWarning, @noDaySecondWorning,@defBTWinvoicing,@defLedgerPrice,
                                            @defLedgerIncurance, @defLedgerCancel,@defLedgerCalamitu,@defLedgerMoneyGr,@idDailyFak,@labelSettings,@defTransferingAcc,
                                             @defReservationAcc,@defLedgerCancelation,@myIban, @myBic, @sepaPath,@defFirstPayment,@defLedReservationCost,@debitorReservationAccount,
                                            @defSepaAcc,@userCreated,@dtCreated,@userModified,@dtModified,@defDifferenceAcc) ");

            SqlParameter[] sqlParameter= new SqlParameter[38];

            sqlParameter[0] = new SqlParameter("@yearSettings", SqlDbType.Char);
            sqlParameter[0].Value = model.yearSettings;

            sqlParameter[1] = new SqlParameter("@noPeriods", SqlDbType.Int);
            sqlParameter[1].Value = model.noPeriods;

            sqlParameter[2] = new SqlParameter("@beginBookYear", SqlDbType.DateTime);
            sqlParameter[2].Value = model.beginBookYear;

            sqlParameter[3] = new SqlParameter("@endBookYear", SqlDbType.DateTime);
            sqlParameter[3].Value = model.endBookYear;

            sqlParameter[4] = new SqlParameter("@isVat", SqlDbType.Bit);
            sqlParameter[4].Value = model.isVat;

            sqlParameter[5] = new SqlParameter("@defDebitorAccount", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.defDebitorAccount;

            sqlParameter[6] = new SqlParameter("@defCreditorAccount", SqlDbType.NVarChar);
            sqlParameter[6].Value = model.defCreditorAccount;

            sqlParameter[7] = new SqlParameter("@defVatDebitor", SqlDbType.NVarChar);
            sqlParameter[7].Value = model.defVatDebitor;

            sqlParameter[8] = new SqlParameter("@defVatCreditor", SqlDbType.NVarChar);
            sqlParameter[8].Value = model.defVatCreditor;

            sqlParameter[9] = new SqlParameter("@currDeferenceAccount", SqlDbType.NVarChar);
            sqlParameter[9].Value = model.currDeferenceAccount;

            sqlParameter[10] = new SqlParameter("@paymentDiferenceAccount", SqlDbType.NVarChar);
            sqlParameter[10].Value = model.paymentDiferenceAccount;

            sqlParameter[11] = new SqlParameter("@bankCostAccount", SqlDbType.NVarChar);
            sqlParameter[11].Value = model.bankCostAccount;

            sqlParameter[12] = new SqlParameter("@defPayCondition", SqlDbType.Int);
            sqlParameter[12].Value = model.defPayCondition;

            sqlParameter[13] = new SqlParameter("@noDayFrstWarning", SqlDbType.Int);
            sqlParameter[13].Value = model.noDayFrstWarning;

            sqlParameter[14] = new SqlParameter("@noDaySecondWorning", SqlDbType.Int);
            sqlParameter[14].Value = model.noDaySecondWorning;

            sqlParameter[15] = new SqlParameter("@defBTWinvoicing", SqlDbType.Int);
            sqlParameter[15].Value = model.defBTWinvoicing;

            sqlParameter[16] = new SqlParameter("@defLedgerPrice", SqlDbType.NVarChar);
            sqlParameter[16].Value = model.defLedgerPrice;
            sqlParameter[17] = new SqlParameter("@defLedgerIncurance", SqlDbType.NVarChar);
            sqlParameter[17].Value = model.defLedgerIncurance;
            sqlParameter[18] = new SqlParameter("@defLedgerCancel", SqlDbType.NVarChar);
            sqlParameter[18].Value = model.defLedgerCancel;
            sqlParameter[19] = new SqlParameter("@defLedgerCalamitu", SqlDbType.NVarChar);
            sqlParameter[19].Value = model.defLedgerCalamitu;
            sqlParameter[20] = new SqlParameter("@defLedgerMoneyGr", SqlDbType.NVarChar);
            sqlParameter[20].Value = model.defLedgerMoneyGr;
            sqlParameter[21] = new SqlParameter("@idDailyFak", SqlDbType.Int);
            sqlParameter[21].Value = model.idDailyFak;
            sqlParameter[22] = new SqlParameter("@labelSettings", SqlDbType.Int);
            sqlParameter[22].Value = model.labelSettings;
            sqlParameter[23] = new SqlParameter("@defTransferingAcc", SqlDbType.NVarChar);
            sqlParameter[23].Value = model.defTransferingAcc;

            sqlParameter[24] = new SqlParameter("@defReservationAcc", SqlDbType.NVarChar);
            sqlParameter[24].Value = model.defReservationAcc;
            sqlParameter[25] = new SqlParameter("@defLedgerCancelation", SqlDbType.NVarChar);
            sqlParameter[25].Value = model.defLedgerCancelation;

            sqlParameter[26] = new SqlParameter("@myIban", SqlDbType.NVarChar);
            sqlParameter[26].Value = model.myIban;
            sqlParameter[27] = new SqlParameter("@myBic", SqlDbType.NVarChar);
            sqlParameter[27].Value = model.myBic;
            sqlParameter[28] = new SqlParameter("@sepaPath", SqlDbType.NVarChar);
            sqlParameter[28].Value = model.sepaPath;
            sqlParameter[29] = new SqlParameter("@defFirstPayment", SqlDbType.NVarChar);
            sqlParameter[29].Value = model.defFirstPayment;
            sqlParameter[30] = new SqlParameter("@defLedReservationCost", SqlDbType.NVarChar);
            sqlParameter[30].Value = model.defLedReservationCost;
            sqlParameter[31] = new SqlParameter("@debitorReservationAccount", SqlDbType.NVarChar);
            sqlParameter[31].Value = model.debitorReservationAccount;
            sqlParameter[32] = new SqlParameter("@defSepaAcc", SqlDbType.NVarChar);
            sqlParameter[32].Value = model.defSepaAcc;

            sqlParameter[33] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[33].Value = model.userCreated;
            sqlParameter[34] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[34].Value = DateTime.Now; //model.dtCreated;
            sqlParameter[35] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[35].Value = model.userModified;
            sqlParameter[36] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[36].Value = model.dtModified;
            sqlParameter[37] = new SqlParameter("@defDifferenceAcc", SqlDbType.NVarChar);
            sqlParameter[37].Value = model.defDifferenceAcc;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccSettings") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idSettings";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccSettings";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Update(AccSettingsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@" UPDATE AccSettings 
                                            SET yearSettings = @yearSettings, noPeriods = @noPeriods, beginBookYear = @beginBookYear, endBookYear = @endBookYear,
                                              isVat = @isVat, defDebitorAccount = @defDebitorAccount, defCreditorAccount = @defCreditorAccount,
                                              defVatDebitor = @defVatDebitor, defVatCreditor = @defVatCreditor, currDeferenceAccount = @currDeferenceAccount,
                                              paymentDiferenceAccount = @paymentDiferenceAccount, bankCostAccount = @bankCostAccount, defPayCondition = @defPayCondition,
                                              noDayFrstWarning = @noDayFrstWarning, noDaySecondWorning = @noDaySecondWorning,defBTWinvoicing=@defBTWinvoicing,
                                              defLedgerPrice=@defLedgerPrice,defLedgerIncurance=@defLedgerIncurance, defLedgerCancel=@defLedgerCancel,
                                              defLedgerCalamitu=@defLedgerCalamitu,defLedgerMoneyGr=@defLedgerMoneyGr,idDailyFak=@idDailyFak,labelSettings=@labelSettings, 
                                              defTransferingAcc=@defTransferingAcc,defReservationAcc=@defReservationAcc,defLedgerCancelation=@defLedgerCancelation,
                                              myIban=@myIban, myBic=@myBic, sepaPath=@sepaPath,defFirstPayment=@defFirstPayment,defLedReservationCost=@defLedReservationCost,
                                              debitorReservationAccount=@debitorReservationAccount,defSepaAcc=@defSepaAcc,
                                              userCreated=@userCreated,dtCreated=@dtCreated,userModified=@userModified,dtModified=@dtModified,defDifferenceAcc=@defDifferenceAcc
                                            WHERE idSettings=@idSettings ");

            SqlParameter[] sqlParameter=new SqlParameter[39];

            sqlParameter[0] = new SqlParameter("@idSettings", SqlDbType.Int);
            sqlParameter[0].Value = model.idSettings;

            sqlParameter[1] = new SqlParameter("@yearSettings", SqlDbType.Char);
            sqlParameter[1].Value = model.yearSettings;

            sqlParameter[2] = new SqlParameter("@noPeriods", SqlDbType.Int);
            sqlParameter[2].Value = model.noPeriods;

            sqlParameter[3] = new SqlParameter("@beginBookYear", SqlDbType.DateTime);
            sqlParameter[3].Value = model.beginBookYear;

            sqlParameter[4] = new SqlParameter("@endBookYear", SqlDbType.DateTime);
            sqlParameter[4].Value = model.endBookYear;

            sqlParameter[5] = new SqlParameter("@isVat", SqlDbType.Bit);
            sqlParameter[5].Value = model.isVat;

            sqlParameter[6] = new SqlParameter("@defDebitorAccount", SqlDbType.NVarChar);
            sqlParameter[6].Value = model.defDebitorAccount;

            sqlParameter[7] = new SqlParameter("@defCreditorAccount", SqlDbType.NVarChar);
            sqlParameter[7].Value = model.defCreditorAccount;

            sqlParameter[8] = new SqlParameter("@defVatDebitor", SqlDbType.NVarChar);
            sqlParameter[8].Value = model.defVatDebitor;

            sqlParameter[9] = new SqlParameter("@defVatCreditor", SqlDbType.NVarChar);
            sqlParameter[9].Value = model.defVatCreditor;

            sqlParameter[10] = new SqlParameter("@currDeferenceAccount", SqlDbType.NVarChar);
            sqlParameter[10].Value = model.currDeferenceAccount;

            sqlParameter[11] = new SqlParameter("@paymentDiferenceAccount", SqlDbType.NVarChar);
            sqlParameter[11].Value = model.paymentDiferenceAccount;

            sqlParameter[12] = new SqlParameter("@bankCostAccount", SqlDbType.NVarChar);
            sqlParameter[12].Value = model.bankCostAccount;

            sqlParameter[13] = new SqlParameter("@defPayCondition", SqlDbType.Int);
            sqlParameter[13].Value = model.defPayCondition;

            sqlParameter[14] = new SqlParameter("@noDayFrstWarning", SqlDbType.Int);
            sqlParameter[14].Value = model.noDayFrstWarning;

            sqlParameter[15] = new SqlParameter("@noDaySecondWorning", SqlDbType.Int);
            sqlParameter[15].Value = model.noDaySecondWorning;

            sqlParameter[16] = new SqlParameter("@defBTWinvoicing", SqlDbType.Int);
            sqlParameter[16].Value = model.defBTWinvoicing;

            sqlParameter[17] = new SqlParameter("@defLedgerPrice", SqlDbType.NVarChar);
            sqlParameter[17].Value = model.defLedgerPrice;
            sqlParameter[18] = new SqlParameter("@defLedgerIncurance", SqlDbType.NVarChar);
            sqlParameter[18].Value = model.defLedgerIncurance;
            sqlParameter[19] = new SqlParameter("@defLedgerCancel", SqlDbType.NVarChar);
            sqlParameter[19].Value = model.defLedgerCancel;

            sqlParameter[20] = new SqlParameter("@defLedgerCalamitu", SqlDbType.NVarChar);
            sqlParameter[20].Value = model.defLedgerCalamitu;
            sqlParameter[21] = new SqlParameter("@defLedgerMoneyGr", SqlDbType.NVarChar);
            sqlParameter[21].Value = model.defLedgerMoneyGr;
            sqlParameter[22] = new SqlParameter("@idDailyFak", SqlDbType.Int);
            sqlParameter[22].Value = model.idDailyFak;
            sqlParameter[23] = new SqlParameter("@labelSettings", SqlDbType.Int);
            sqlParameter[23].Value = model.labelSettings;
            sqlParameter[24] = new SqlParameter("@defTransferingAcc", SqlDbType.NVarChar);
            sqlParameter[24].Value = model.defTransferingAcc;

            sqlParameter[25] = new SqlParameter("@defReservationAcc", SqlDbType.NVarChar);
            sqlParameter[25].Value = model.defReservationAcc;
            sqlParameter[26] = new SqlParameter("@defLedgerCancelation", SqlDbType.NVarChar);
            sqlParameter[26].Value = model.defLedgerCancelation;

            sqlParameter[27] = new SqlParameter("@myIban", SqlDbType.NVarChar);
            sqlParameter[27].Value = model.myIban;
            sqlParameter[28] = new SqlParameter("@myBic", SqlDbType.NVarChar);
            sqlParameter[28].Value = model.myBic;
            sqlParameter[29] = new SqlParameter("@sepaPath", SqlDbType.NVarChar);
            sqlParameter[29].Value = model.sepaPath;
            sqlParameter[30] = new SqlParameter("@defFirstPayment", SqlDbType.NVarChar);
            sqlParameter[30].Value = model.defFirstPayment;
            sqlParameter[31] = new SqlParameter("@defLedReservationCost", SqlDbType.NVarChar);
            sqlParameter[31].Value = model.defLedReservationCost;
            sqlParameter[32] = new SqlParameter("@debitorReservationAccount", SqlDbType.NVarChar);
            sqlParameter[32].Value = model.debitorReservationAccount;
            sqlParameter[33] = new SqlParameter("@defSepaAcc", SqlDbType.NVarChar);
            sqlParameter[33].Value = model.defSepaAcc;
            sqlParameter[34] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[34].Value = model.userCreated;
            sqlParameter[35] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[35].Value = model.dtCreated;
            sqlParameter[36] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[36].Value = model.userModified;
            sqlParameter[37] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[37].Value = DateTime.Now; //model.dtModified;
            sqlParameter[38] = new SqlParameter("@defDifferenceAcc", SqlDbType.NVarChar);
            sqlParameter[38].Value = model.defDifferenceAcc;

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
            sqlParameter[4].Value = model.idSettings.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idSettings";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccSettings";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idSettings, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query=string.Format(@" DELETE AccSettings 
                                          WHERE idSettings = @idSettings  ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idSettings", SqlDbType.Int);
            sqlParameter[0].Value = idSettings;

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
            sqlParameter[4].Value = idSettings.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idSettings";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccSettings";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}