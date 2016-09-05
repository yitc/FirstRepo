using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;


namespace BIS.Business
{
    public class LedgerAccountBUS
    {
        private LedgerAccountDAO ledgerDAO;

        public LedgerAccountBUS(string bookyear)
        {
            ledgerDAO = new LedgerAccountDAO(bookyear);
        }

        public bool Save(LedgerAccountModel ledgermodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = ledgerDAO.Save(ledgermodel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public bool Update(LedgerAccountModel ledgermodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = ledgerDAO.Update(ledgermodel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

      
        public List<IModel> GetAllAccounts()
        {
            DataTable dataTable = new DataTable();
            dataTable = ledgerDAO.GetAllAccounts();
            List<IModel> ledgermodel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    LedgerAccountModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new LedgerAccountModel();
                        model.idLedgerAccount = Int32.Parse(dr["idLedgerAccount"].ToString());
                        model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        model.descLedgerAccount = dr["descLedgerAccount"].ToString();

                        if (dr["openDebitAccount"].ToString() != "")
                            model.openDebitAccount = Decimal.Parse(dr["openDebitAccount"].ToString());
                        if (dr["openCreditAccount"].ToString() != "")
                            model.openCreditAccount = Decimal.Parse(dr["openCreditAccount"].ToString());
                        if (dr["accountTypeAccount"].ToString() != "")
                            model.accountTypeAccount = Decimal.Parse(dr["accountTypeAccount"].ToString());
                        if (dr["nameTypeAccount"].ToString() != "")
                            model.nameTypeAccount = dr["nameTypeAccount"].ToString();
                        //       model.nameTypeAccount = dr["nameTypeAccount"].ToString();
                        if (dr["idCostCenter"].ToString() != "")
                            model.idCostCenter = dr["idCostCenter"].ToString();
                        if (dr["nameCostLedgerAccount"].ToString() != "")
                            model.nameCostLedgerAccount = dr["nameCostLedgerAccount"].ToString();
                        model.mandatoryCostAccount = Boolean.Parse(dr["mandatoryCostAccount"].ToString());
                        model.mandatoryDebitorAccount = Boolean.Parse(dr["mandatoryDebitorAccount"].ToString());
                        model.mandatoryCreditorAccount = Boolean.Parse(dr["mandatoryCreditorAccount"].ToString());
                        model.mandatoryProjectAccount = Boolean.Parse(dr["mandatoryProjectAccount"].ToString());
                        model.isBudgetAccount = Boolean.Parse(dr["isBudgetAccount"].ToString());

                        if (dr["class1Account"].ToString() != "")
                            model.class1Account = Int32.Parse(dr["class1Account"].ToString());

                        if (dr["class2Account"].ToString() != "")
                            model.class2Account = Int32.Parse(dr["class2Account"].ToString());

                        if (dr["class3Account"].ToString() != "")
                            model.class3Account = Int32.Parse(dr["class3Account"].ToString());

                        if (dr["class4Account"].ToString() != "")
                            model.class4Account = Int32.Parse(dr["class4Account"].ToString());

                        if (dr["class5Account"].ToString() != "")
                            model.class5Account = Int32.Parse(dr["class5Account"].ToString());

                        if (dr["debitAccount"].ToString() != "")
                            model.debitAccount = Decimal.Parse(dr["debitAccount"].ToString());
                        if (dr["creditAccount"].ToString() != "")
                            model.creditAccount = Decimal.Parse(dr["creditAccount"].ToString());
                        if (dr["transactionNoAccount"].ToString() != "")
                            model.transactionNoAccount = Int32.Parse(dr["transactionNoAccount"].ToString());
                        //if (dr["nameTypeAccount"].ToString() != "")
                        //    model.nameTypeAccount = dr["nameTypeAccount"].ToString();
                        if (dr["nameClass1LedgerAccount"].ToString() != "")
                            model.nameClass1LedgerAccount = dr["nameClass1LedgerAccount"].ToString();
                        if (dr["nameClass2LedgerAccount"].ToString() != "")
                            model.nameClass2LedgerAccount = dr["nameClass2LedgerAccount"].ToString();
                        if (dr["nameClass3LedgerAccount"].ToString() != "")
                            model.nameClass3LedgerAccount = dr["nameClass3LedgerAccount"].ToString();
                        if (dr["nameClass4LedgerAccount"].ToString() != "")
                            model.nameClass4LedgerAccount = dr["nameClass4LedgerAccount"].ToString();
                        if (dr["nameClass5LedgerAccount"].ToString() != "")
                            model.nameClass5LedgerAccount = dr["nameClass5LedgerAccount"].ToString();
                        if (dr["valutaDebitLedgerAccount"].ToString() != "")
                            model.valutaDebitLedgerAccount = Decimal.Parse(dr["valutaDebitLedgerAccount"].ToString());
                        if (dr["valutaCreditLedgerAccount"].ToString() != "")
                            model.valutaCreditLedgerAccount = Decimal.Parse(dr["valutaCreditLedgerAccount"].ToString());
                        model.isBTWLedgerAccount = Boolean.Parse(dr["isBTWLedgerAccount"].ToString());
                        model.isActiveLedgerAccount = Boolean.Parse(dr["isActiveLedgerAccount"].ToString());

                        if (dr["sideBooking"].ToString() != "")
                            model.sideBooking = dr["sideBooking"].ToString();
                        model.isBlockMemorial = Boolean.Parse(dr["isBlockMemorial"].ToString());
                        if (dr["btwId"].ToString() != "")
                            model.btwId = Int32.Parse(dr["btwId"].ToString());

                        ledgermodel.Add(model);
                    }
                    return ledgermodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public LedgerAccountModel GetAccount(string numberLedgerAccount, string year)
        {
            DataTable dataTable = new DataTable();
            dataTable = ledgerDAO.GetAccount(numberLedgerAccount, year);
            LedgerAccountModel ledgermodel = new LedgerAccountModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    LedgerAccountModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new LedgerAccountModel();
                        model.idLedgerAccount = Int32.Parse(dr["idLedgerAccount"].ToString());
                        model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        model.descLedgerAccount = dr["descLedgerAccount"].ToString();

                        if (dr["openDebitAccount"].ToString() != "")
                            model.openDebitAccount = Decimal.Parse(dr["openDebitAccount"].ToString());
                        if (dr["openCreditAccount"].ToString() != "")
                            model.openCreditAccount = Decimal.Parse(dr["openCreditAccount"].ToString());
                        if (dr["accountTypeAccount"].ToString() != "")
                            model.accountTypeAccount = Decimal.Parse(dr["accountTypeAccount"].ToString());
                        if (dr["nameTypeAccount"].ToString() != "")
                            model.nameTypeAccount = dr["nameTypeAccount"].ToString();
                        //       model.nameTypeAccount = dr["nameTypeAccount"].ToString();
                        if (dr["idCostCenter"].ToString() != "")
                            model.idCostCenter = dr["idCostCenter"].ToString();
                        if (dr["nameCostLedgerAccount"].ToString() != "")
                            model.nameCostLedgerAccount = dr["nameCostLedgerAccount"].ToString();
                        model.mandatoryCostAccount = Boolean.Parse(dr["mandatoryCostAccount"].ToString());
                        model.mandatoryDebitorAccount = Boolean.Parse(dr["mandatoryDebitorAccount"].ToString());
                        model.mandatoryCreditorAccount = Boolean.Parse(dr["mandatoryCreditorAccount"].ToString());
                        model.mandatoryProjectAccount = Boolean.Parse(dr["mandatoryProjectAccount"].ToString());
                        model.isBudgetAccount = Boolean.Parse(dr["isBudgetAccount"].ToString());

                        if (dr["class1Account"].ToString() != "")
                            model.class1Account = Int32.Parse(dr["class1Account"].ToString());
                        if (dr["class2Account"].ToString() != "")
                            model.class2Account = Int32.Parse(dr["class2Account"].ToString());
                        if (dr["class3Account"].ToString() != "")
                            model.class3Account = Int32.Parse(dr["class3Account"].ToString());
                        if (dr["class4Account"].ToString() != "")
                            model.class4Account = Int32.Parse(dr["class4Account"].ToString());
                        if (dr["class5Account"].ToString() != "")
                            model.class5Account = Int32.Parse(dr["class5Account"].ToString());
                        if (dr["debitAccount"].ToString() != "")
                            model.debitAccount = Decimal.Parse(dr["debitAccount"].ToString());
                        if (dr["creditAccount"].ToString() != "")
                            model.creditAccount = Decimal.Parse(dr["creditAccount"].ToString());
                        if (dr["transactionNoAccount"].ToString() != "")
                            model.transactionNoAccount = Int32.Parse(dr["transactionNoAccount"].ToString());
                        //if (dr["nameTypeAccount"].ToString() != "")
                        //    model.nameTypeAccount = dr["nameTypeAccount"].ToString();
                        if (dr["nameClass1LedgerAccount"].ToString() != "")
                            model.nameClass1LedgerAccount = dr["nameClass1LedgerAccount"].ToString();
                        if (dr["nameClass2LedgerAccount"].ToString() != "")
                            model.nameClass2LedgerAccount = dr["nameClass2LedgerAccount"].ToString();
                        if (dr["nameClass3LedgerAccount"].ToString() != "")
                            model.nameClass3LedgerAccount = dr["nameClass3LedgerAccount"].ToString();
                        if (dr["nameClass4LedgerAccount"].ToString() != "")
                            model.nameClass4LedgerAccount = dr["nameClass4LedgerAccount"].ToString();
                        if (dr["nameClass5LedgerAccount"].ToString() != "")
                            model.nameClass5LedgerAccount = dr["nameClass5LedgerAccount"].ToString();
                        if (dr["valutaDebitLedgerAccount"].ToString() != "")
                            model.valutaDebitLedgerAccount = Decimal.Parse(dr["valutaDebitLedgerAccount"].ToString());
                        if (dr["valutaCreditLedgerAccount"].ToString() != "")
                            model.valutaCreditLedgerAccount = Decimal.Parse(dr["valutaCreditLedgerAccount"].ToString());
                        model.isBTWLedgerAccount = Boolean.Parse(dr["isBTWLedgerAccount"].ToString());
                        model.isActiveLedgerAccount = Boolean.Parse(dr["isActiveLedgerAccount"].ToString());
                        if (dr["sideBooking"].ToString() != "")
                            model.sideBooking = dr["sideBooking"].ToString();
                        model.isBlockMemorial = Boolean.Parse(dr["isBlockMemorial"].ToString());
                        if (dr["btwId"].ToString() != "")
                            model.btwId = Int32.Parse(dr["btwId"].ToString());

                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        // ledgermodel.Add(model);
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        //provera radi brisanja
        public List<LedgerAccountModel> GetCostCenterFromAccLedgerAccount(string idCostCenter)
        {
            DataTable dataTable = new DataTable();
            dataTable = ledgerDAO.GetGetCostCenterFromAccLedgerAccount(idCostCenter);
            List<LedgerAccountModel> arrange = new List<LedgerAccountModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    LedgerAccountModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new LedgerAccountModel();
                        if (dr["idCostCenter"].ToString() != "")
                        {
                            model.idCostCenter = dr["idCostCenter"].ToString();
                        }

                        arrange.Add(model);
                    }
                    return arrange;
                }
                else
                    return null;
            }
            else
                return null;

        }



       


    }
}
