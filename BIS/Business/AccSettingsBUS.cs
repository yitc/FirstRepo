using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;

namespace BIS.Business
{
    public class AccSettingsBUS
    {
        private AccSettingsDAO accSettingsDAO;

        public AccSettingsBUS()
        {
            accSettingsDAO = new AccSettingsDAO();
        }

        public bool Save(AccSettingsModel accSettings, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accSettingsDAO.Save(accSettings,nameForm,idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(AccSettingsModel accSettings, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accSettingsDAO.Update(accSettings, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accSettingsDAO.Delete(id, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool ClientInvoice( string client,string invoice, string yearb)
        {
            bool retval = false;
            try
            {
                retval = accSettingsDAO.ClientInvoice(client, invoice, yearb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<IModel> GetAllAccountSettings(string year)
        {
            DataTable dataTable = new DataTable();
            dataTable = accSettingsDAO.GetAllAccSettings(year);

            List<IModel> accountSettingsmodel = new List<IModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {                  

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AccSettingsModel model = new AccSettingsModel();

                        model.idSettings = Int32.Parse(dr["idSettings"].ToString());
                        model.yearSettings = dr["yearSettings"].ToString();

                        if (dr["noPeriods"].ToString() != "")
                        {
                            model.noPeriods = Int32.Parse(dr["noPeriods"].ToString());
                        }

                        if (dr["beginBookYear"].ToString() != "")
                        {
                            model.beginBookYear = Convert.ToDateTime(dr["beginBookYear"].ToString());
                        }

                        if (dr["endBookYear"].ToString() != "")
                        {
                            model.endBookYear = Convert.ToDateTime(dr["endBookYear"].ToString());
                        }
                        model.isVat = bool.Parse(dr["isVat"].ToString());
                        model.defDebitorAccount = dr["defDebitorAccount"].ToString();
                        model.defCreditorAccount = dr["defCreditorAccount"].ToString();
                        model.defVatDebitor = dr["defVatDebitor"].ToString();
                        model.defVatCreditor = dr["defVatCreditor"].ToString();
                        model.currDeferenceAccount = dr["currDeferenceAccount"].ToString();
                        model.paymentDiferenceAccount = dr["paymentDiferenceAccount"].ToString();
                        model.bankCostAccount = dr["bankCostAccount"].ToString();

                        if (dr["defPayCondition"].ToString() != "")
                        {
                            model.defPayCondition = Int32.Parse(dr["defPayCondition"].ToString());
                        }
                        if (dr["noDayFrstWarning"].ToString() != "")
                           model.noDayFrstWarning = Int32.Parse(dr["noDayFrstWarning"].ToString());

                        if (dr["noDaySecondWorning"].ToString() != "")
                        {
                            model.noDaySecondWorning = Int32.Parse(dr["noDaySecondWorning"].ToString());
                        }
                        if (dr["defBTWinvoicing"].ToString() != "")
                        {
                            model.defBTWinvoicing = Int32.Parse(dr["defBTWinvoicing"].ToString());
                        }
                        model.defLedgerPrice = dr["defLedgerPrice"].ToString();
                        model.defLedgerIncurance = dr["defLedgerIncurance"].ToString();
                        model.defLedgerCancel = dr["defLedgerCancel"].ToString();

                        model.defLedgerCalamitu = dr["defLedgerCalamitu"].ToString();
                        model.defLedgerMoneyGr = dr["defLedgerMoneyGr"].ToString();
                        if (dr["idDailyFak"].ToString() != "")
                            model.idDailyFak = Int32.Parse(dr["idDailyFak"].ToString());
                        if (dr["labelSettings"].ToString() != "")
                            model.labelSettings = Int32.Parse(dr["labelSettings"].ToString());
                        model.defTransferingAcc = dr["defTransferingAcc"].ToString();

                        model.defReservationAcc = dr["defReservationAcc"].ToString();
                        model.defLedgerCancelation = dr["defLedgerCancelation"].ToString();

                        model.myIban = dr["myIban"].ToString();
                        model.myBic = dr["myBic"].ToString();
                        model.sepaPath = dr["sepaPath"].ToString();

                        model.defFirstPayment = dr["defFirstPayment"].ToString();
                        model.defLedReservationCost = dr["defLedReservationCost"].ToString();
                        model.debitorReservationAccount = dr["debitorReservationAccount"].ToString();
                        model.defSepaAcc = dr["defSepaAcc"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        model.defDifferenceAcc = dr["defDifferenceAcc"].ToString();


                        accountSettingsmodel.Add(model);
                    }
                    return accountSettingsmodel;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccSettingsModel> GetAllAccSettings(string year)
        {
            DataTable dataTable = new DataTable();
            dataTable = accSettingsDAO.GetAllAccSettings(year);
            List<AccSettingsModel> compList = new List<AccSettingsModel>();


            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {

                        AccSettingsModel model = new AccSettingsModel();

                        model.idSettings = Int32.Parse(dr["idSettings"].ToString());
                        model.yearSettings = dr["yearSettings"].ToString();

                        if (dr["noPeriods"].ToString() != "")
                        {
                            model.noPeriods = Int32.Parse(dr["noPeriods"].ToString());
                        }

                        if (dr["beginBookYear"].ToString() != "")
                        {
                            model.beginBookYear = DateTime.Parse(dr["beginBookYear"].ToString());
                        }

                        if (dr["endBookYear"].ToString() != "")
                        {
                            model.endBookYear = DateTime.Parse(dr["endBookYear"].ToString());
                        }
                        model.isVat = bool.Parse(dr["isVat"].ToString());
                        model.defDebitorAccount = dr["defDebitorAccount"].ToString();
                        model.defCreditorAccount = dr["defCreditorAccount"].ToString();
                        model.defVatDebitor = dr["defVatDebitor"].ToString();
                        model.defVatCreditor = dr["defVatCreditor"].ToString();
                        model.currDeferenceAccount = dr["currDeferenceAccount"].ToString();
                        model.paymentDiferenceAccount = dr["paymentDiferenceAccount"].ToString();
                        model.bankCostAccount = dr["bankCostAccount"].ToString();

                        if (dr["defPayCondition"].ToString() != "")
                        {
                            model.defPayCondition = Int32.Parse(dr["defPayCondition"].ToString());
                        }

                        if (dr["noDayFrstWarning"].ToString() != "")
                        {
                            model.noDayFrstWarning = Int32.Parse(dr["noDayFrstWarning"].ToString());
                        }

                        if (dr["noDaySecondWorning"].ToString() != "")
                        {
                            model.noDaySecondWorning = Int32.Parse(dr["noDaySecondWorning"].ToString());
                        }
                        if (dr["defBTWinvoicing"].ToString() != "")
                        {
                            model.defBTWinvoicing = Int32.Parse(dr["defBTWinvoicing"].ToString());
                        }
                        model.defLedgerPrice = dr["defLedgerPrice"].ToString();
                        model.defLedgerIncurance = dr["defLedgerIncurance"].ToString();
                        model.defLedgerCancel = dr["defLedgerCancel"].ToString();
                        model.defLedgerCalamitu = dr["defLedgerCalamitu"].ToString();
                        model.defLedgerMoneyGr = dr["defLedgerMoneyGr"].ToString();
                        if (dr["idDailyFak"].ToString() != "")
                            model.idDailyFak = Int32.Parse(dr["idDailyFak"].ToString());
                        if (dr["labelSettings"].ToString() != "")
                            model.labelSettings = Int32.Parse(dr["labelSettings"].ToString());
                        model.defTransferingAcc = dr["defTransferingAcc"].ToString();
                        model.defReservationAcc = dr["defReservationAcc"].ToString();
                        model.defLedgerCancelation = dr["defLedgerCancelation"].ToString();
                        model.myIban = dr["myIban"].ToString();
                        model.myBic = dr["myBic"].ToString();
                        model.sepaPath = dr["sepaPath"].ToString();

                        model.defFirstPayment = dr["defFirstPayment"].ToString();
                        model.defLedReservationCost = dr["defLedReservationCost"].ToString();
                        model.debitorReservationAccount = dr["debitorReservationAccount"].ToString();
                        model.defSepaAcc = dr["defSepaAcc"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        model.defDifferenceAcc = dr["defDifferenceAcc"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                    return null;
            }
            else return null;
        }

        public AccSettingsModel GetSettingsByID(string idSettings)
        {
            DataTable dataTable = new DataTable();
            dataTable = accSettingsDAO.GetAccSettingsByID(idSettings);
            AccSettingsModel accSettings = new AccSettingsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccSettingsModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccSettingsModel();                        

                        model.idSettings = Int32.Parse(dr["idSettings"].ToString());
                        model.yearSettings = dr["yearSettings"].ToString();

                        if (dr["noPeriods"].ToString() != "")
                        { 
                        model.noPeriods = Int32.Parse(dr["noPeriods"].ToString());
                        }

                        if (dr["beginBookYear"].ToString() != "")
                        { 
                        model.beginBookYear = DateTime.Parse(dr["beginBookYear"].ToString());
                        }

                        if (dr["endBookYear"].ToString() != "")
                        { 
                        model.endBookYear = DateTime.Parse(dr["endBookYear"].ToString());
                        }

                        model.isVat = bool.Parse(dr["isVat"].ToString());
                        model.defDebitorAccount = dr["defDebitorAccount"].ToString();
                        model.defCreditorAccount = dr["defCreditorAccount"].ToString();
                        model.defVatDebitor = dr["defVatDebitor"].ToString();
                        model.defVatCreditor = dr["defVatCreditor"].ToString();
                        model.currDeferenceAccount = dr["currDeferenceAccount"].ToString();
                        model.paymentDiferenceAccount = dr["paymentDiferenceAccount"].ToString();
                        model.bankCostAccount = dr["bankCostAccount"].ToString();

                        if (dr["defPayCondition"].ToString() != "")
                        { 
                        model.defPayCondition = Int32.Parse(dr["defPayCondition"].ToString());
                        }

                        if (dr["noDayFrstWarning"].ToString() != "")
                        { 
                        model.noDayFrstWarning = Int32.Parse(dr["noDayFrstWarning"].ToString());
                        }

                        if (dr["noDaySecondWorning"].ToString() != "")
                        { 
                        model.noDaySecondWorning = Int32.Parse(dr["noDaySecondWorning"].ToString());
                        }
                        if (dr["defBTWinvoicing"].ToString() != "")
                        {
                            model.defBTWinvoicing = Int32.Parse(dr["defBTWinvoicing"].ToString());
                        }
                        model.defLedgerPrice = dr["defLedgerPrice"].ToString();
                        model.defLedgerIncurance = dr["defLedgerIncurance"].ToString();
                        model.defLedgerCancel = dr["defLedgerCancel"].ToString();
                        model.defLedgerCalamitu = dr["defLedgerCalamitu"].ToString();
                        model.defLedgerMoneyGr = dr["defLedgerMoneyGr"].ToString();
                        if (dr["idDailyFak"].ToString() != "")
                            model.idDailyFak = Int32.Parse(dr["idDailyFak"].ToString());
                        if (dr["labelSettings"].ToString() != "")
                            model.labelSettings = Int32.Parse(dr["labelSettings"].ToString());
                        model.defTransferingAcc = dr["defTransferingAcc"].ToString();
                        model.defReservationAcc = dr["defReservationAcc"].ToString();
                        model.defLedgerCancelation = dr["defLedgerCancelation"].ToString();
                        model.myIban = dr["myIban"].ToString();
                        model.myBic = dr["myBic"].ToString();
                        model.sepaPath = dr["sepaPath"].ToString();

                        model.defFirstPayment = dr["defFirstPayment"].ToString();
                        model.defLedReservationCost = dr["defLedReservationCost"].ToString();
                        model.debitorReservationAccount = dr["debitorReservationAccount"].ToString();
                        model.defSepaAcc = dr["defSepaAcc"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        model.defDifferenceAcc = dr["defDifferenceAcc"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccSettingsModel> GetAllAccS()
        {
            DataTable dataTable = new DataTable();
            dataTable = accSettingsDAO.GetAllAccS();

            List<AccSettingsModel> accountSettingsmodel = new List<AccSettingsModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AccSettingsModel model = new AccSettingsModel();

                        model.idSettings = Int32.Parse(dr["idSettings"].ToString());
                        model.yearSettings = dr["yearSettings"].ToString();

                        if (dr["noPeriods"].ToString() != "")
                        {
                            model.noPeriods = Int32.Parse(dr["noPeriods"].ToString());
                        }

                        if (dr["beginBookYear"].ToString() != "")
                        {
                            model.beginBookYear = Convert.ToDateTime(dr["beginBookYear"].ToString());
                        }

                        if (dr["endBookYear"].ToString() != "")
                        {
                            model.endBookYear = Convert.ToDateTime(dr["endBookYear"].ToString());
                        }
                        model.isVat = bool.Parse(dr["isVat"].ToString());
                        model.defDebitorAccount = dr["defDebitorAccount"].ToString();
                        model.defCreditorAccount = dr["defCreditorAccount"].ToString();
                        model.defVatDebitor = dr["defVatDebitor"].ToString();
                        model.defVatCreditor = dr["defVatCreditor"].ToString();
                        model.currDeferenceAccount = dr["currDeferenceAccount"].ToString();
                        model.paymentDiferenceAccount = dr["paymentDiferenceAccount"].ToString();
                        model.bankCostAccount = dr["bankCostAccount"].ToString();

                        if (dr["defPayCondition"].ToString() != "")
                        {
                            model.defPayCondition = Int32.Parse(dr["defPayCondition"].ToString());
                        }
                        if (dr["noDayFrstWarning"].ToString() != "")
                            model.noDayFrstWarning = Int32.Parse(dr["noDayFrstWarning"].ToString());

                        if (dr["noDaySecondWorning"].ToString() != "")
                        {
                            model.noDaySecondWorning = Int32.Parse(dr["noDaySecondWorning"].ToString());
                        }
                        if (dr["defBTWinvoicing"].ToString() != "")
                        {
                            model.defBTWinvoicing = Int32.Parse(dr["defBTWinvoicing"].ToString());
                        }
                        model.defLedgerPrice = dr["defLedgerPrice"].ToString();
                        model.defLedgerIncurance = dr["defLedgerIncurance"].ToString();
                        model.defLedgerCancel = dr["defLedgerCancel"].ToString();

                        model.defLedgerCalamitu = dr["defLedgerCalamitu"].ToString();
                        model.defLedgerMoneyGr = dr["defLedgerMoneyGr"].ToString();
                        if (dr["idDailyFak"].ToString() != "")
                            model.idDailyFak = Int32.Parse(dr["idDailyFak"].ToString());
                        if (dr["labelSettings"].ToString() != "")
                            model.labelSettings = Int32.Parse(dr["labelSettings"].ToString());
                        model.defTransferingAcc = dr["defTransferingAcc"].ToString();

                        model.defReservationAcc = dr["defReservationAcc"].ToString();
                        model.defLedgerCancelation = dr["defLedgerCancelation"].ToString();

                        model.myIban = dr["myIban"].ToString();
                        model.myBic = dr["myBic"].ToString();
                        model.sepaPath = dr["sepaPath"].ToString();

                        model.defFirstPayment = dr["defFirstPayment"].ToString();
                        model.defLedReservationCost = dr["defLedReservationCost"].ToString();
                        model.debitorReservationAccount = dr["debitorReservationAccount"].ToString();
                        model.defSepaAcc = dr["defSepaAcc"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        model.defDifferenceAcc = dr["defDifferenceAcc"].ToString();


                        accountSettingsmodel.Add(model);
                    }
                    return accountSettingsmodel;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}