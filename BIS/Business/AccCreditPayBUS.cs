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
    public class AccCreditPayBUS
    {
        private AccCreditPayDAO creditpayDAO;

        public AccCreditPayBUS()
        {
            creditpayDAO = new AccCreditPayDAO();
        }

        public int Save(AccCreditPayModel model, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = creditpayDAO.Save(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public bool Update(AccCreditPayModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = creditpayDAO.Update(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool UpdateApproved(AccCreditPayModel model, int status, int user, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = creditpayDAO.UpdateApproved(model, status, user, nameForm, idUser);

            }
            catch (Exception ex)
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

                retval = creditpayDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
 

        public List<AccCreditPayModel> GetAllPays()
        {
            DataTable dataTable = new DataTable();
            dataTable = creditpayDAO.GetAllPays();
            List<AccCreditPayModel> creditmodel = new List<AccCreditPayModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccCreditPayModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccCreditPayModel();

                         if (dr["isSelected"].ToString() != "")
                            model.isSelected = Boolean.Parse(dr["isSelected"].ToString());
                        model.idCreditPay = Int32.Parse(dr["idCreditPay"].ToString());
                        if (dr["dtItem"].ToString() != "")
                            model.dtItem = DateTime.Parse(dr["dtItem"].ToString());
                        if (dr["dtValuta"].ToString() != "")
                            model.dtValuta = DateTime.Parse(dr["dtValuta"].ToString());
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["account"].ToString() != "")
                            model.account = dr["account"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["inkopNr"].ToString() != "")
                            model.inkopNr = dr["inkopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["descItem"].ToString() != "")
                            model.descItem = dr["descItem"].ToString();
                        if (dr["amountC"].ToString() != "")
                            model.amountC = Decimal.Parse(dr["amountC"].ToString());
                        if (dr["amountInCurr"].ToString() != "")
                            model.amountInCurr = Decimal.Parse(dr["amountInCurr"].ToString());
                        if (dr["idBtw"].ToString() != "")
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        if (dr["currency"].ToString() != "")
                            model.currency = dr["currency"].ToString();
                        if (dr["cost"].ToString() != "")
                            model.cost = dr["cost"].ToString();
                        if (dr["project"].ToString() != "")
                            model.project = dr["project"].ToString();
                        if (dr["isApproved"].ToString() != "")
                            model.isApproved = Boolean.Parse(dr["isApproved"].ToString());
                        if (dr["isBooked"].ToString() != "")
                            model.isBooked = Boolean.Parse(dr["isBooked"].ToString());
                        if (dr["isSent"].ToString() != "")
                            model.isSent = Boolean.Parse(dr["isSent"].ToString());
                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());
                         if (dr["namefile"].ToString() != "")
                            model.namefile = dr["namefile"].ToString();
                         if (dr["approvedUser"].ToString() != "")
                            model.approvedUser = Int32.Parse(dr["approvedUser"].ToString());
                         if (dr["createUser"].ToString() != "")
                            model.createUser = Int32.Parse(dr["createUser"].ToString());
                         if (dr["dtCreation"].ToString() != "")
                            model.dtCreation = DateTime.Parse(dr["dtCreation"].ToString());
                         if (dr["payIban"].ToString() != "")
                            model.payIban = dr["payIban"].ToString();
                         if (dr["idDocument"].ToString() != "")
                             model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                         if (dr["idOption"].ToString() != "")
                             model.idOption = Int32.Parse(dr["idOption"].ToString());
                         if (dr["amountD"].ToString() != "")
                             model.amountD = Decimal.Parse(dr["amountD"].ToString());
                         if (dr["paydays"].ToString() != "")
                             model.paydays = Int32.Parse(dr["paydays"].ToString());
                         if (dr["idTask"].ToString() != "")
                             model.idTask = Int32.Parse(dr["idTask"].ToString());
                         if (dr["isAprBook"].ToString() != "")
                             model.isAprBook = Boolean.Parse(dr["isAprBook"].ToString());
                         //================================================================
                         if (dr["userCreated"].ToString() != "")
                             model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                         if (dr["dtCreated"].ToString() != "")
                             model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                         if (dr["userModified"].ToString() != "")
                             model.userModified = Int32.Parse(dr["userModified"].ToString());
                         if (dr["dtModified"].ToString() != "")
                             model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                         //================================================================

                        creditmodel.Add(model);
                    }
                    return creditmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccCreditPayModel> GetAllPaysApproved()
        {
            DataTable dataTable = new DataTable();
            dataTable = creditpayDAO.GetAllPaysApproved();
            List<AccCreditPayModel> creditmodel = new List<AccCreditPayModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccCreditPayModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccCreditPayModel();

                        if (dr["isSelected"].ToString() != "")
                            model.isSelected = Boolean.Parse(dr["isSelected"].ToString());
                        model.idCreditPay = Int32.Parse(dr["idCreditPay"].ToString());
                        if (dr["dtItem"].ToString() != "")
                            model.dtItem = DateTime.Parse(dr["dtItem"].ToString());
                        if (dr["dtValuta"].ToString() != "")
                            model.dtValuta = DateTime.Parse(dr["dtValuta"].ToString());
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["account"].ToString() != "")
                            model.account = dr["account"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["inkopNr"].ToString() != "")
                            model.inkopNr = dr["inkopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["descItem"].ToString() != "")
                            model.descItem = dr["descItem"].ToString();
                        if (dr["amountC"].ToString() != "")
                            model.amountC = Decimal.Parse(dr["amountC"].ToString());
                        if (dr["amountInCurr"].ToString() != "")
                            model.amountInCurr = Decimal.Parse(dr["amountInCurr"].ToString());
                        if (dr["idBtw"].ToString() != "")
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        if (dr["currency"].ToString() != "")
                            model.currency = dr["currency"].ToString();
                        if (dr["cost"].ToString() != "")
                            model.cost = dr["cost"].ToString();
                        if (dr["project"].ToString() != "")
                            model.project = dr["project"].ToString();
                        if (dr["isApproved"].ToString() != "")
                            model.isApproved = Boolean.Parse(dr["isApproved"].ToString());
                        if (dr["isBooked"].ToString() != "")
                            model.isBooked = Boolean.Parse(dr["isBooked"].ToString());
                        if (dr["isSent"].ToString() != "")
                            model.isSent = Boolean.Parse(dr["isSent"].ToString());
                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());
                        if (dr["namefile"].ToString() != "")
                            model.namefile = dr["namefile"].ToString();
                        if (dr["approvedUser"].ToString() != "")
                            model.approvedUser = Int32.Parse(dr["approvedUser"].ToString());
                        if (dr["createUser"].ToString() != "")
                            model.createUser = Int32.Parse(dr["createUser"].ToString());
                        if (dr["dtCreation"].ToString() != "")
                            model.dtCreation = DateTime.Parse(dr["dtCreation"].ToString());
                        if (dr["payIban"].ToString() != "")
                            model.payIban = dr["payIban"].ToString();
                        if (dr["idDocument"].ToString() != "")
                            model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                        if (dr["idOption"].ToString() != "")
                            model.idOption = Int32.Parse(dr["idOption"].ToString());
                        if (dr["amountD"].ToString() != "")
                            model.amountD = Decimal.Parse(dr["amountD"].ToString());
                        if (dr["paydays"].ToString() != "")
                            model.paydays = Int32.Parse(dr["paydays"].ToString());
                        if (dr["idTask"].ToString() != "")
                            model.idTask = Int32.Parse(dr["idTask"].ToString());
                        if (dr["isAprBook"].ToString() != "")
                            model.isAprBook = Boolean.Parse(dr["isAprBook"].ToString());
                        //================================================================
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        //================================================================

                        creditmodel.Add(model);
                    }
                    return creditmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccCreditPayModel> GetBankPayByDate(DateTime dateval, DateTime dateplus)
        {
            DataTable dataTable = new DataTable();
            dataTable = creditpayDAO.GetBankPayByDate(dateval, dateplus);
            List<AccCreditPayModel> creditmodel = new List<AccCreditPayModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccCreditPayModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccCreditPayModel();

                        if (dr["isSelected"].ToString() != "")
                            model.isSelected = Boolean.Parse(dr["isSelected"].ToString());
                        model.idCreditPay = Int32.Parse(dr["idCreditPay"].ToString());
                        if (dr["dtItem"].ToString() != "")
                            model.dtItem = DateTime.Parse(dr["dtItem"].ToString());
                        if (dr["dtValuta"].ToString() != "")
                            model.dtValuta = DateTime.Parse(dr["dtValuta"].ToString());
                        if (dr["ndays"].ToString() != "")
                            model.ndays = Int32.Parse(dr["ndays"].ToString());
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["account"].ToString() != "")
                            model.account = dr["account"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["inkopNr"].ToString() != "")
                            model.inkopNr = dr["inkopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["descItem"].ToString() != "")
                            model.descItem = dr["descItem"].ToString();
                        if (dr["amountC"].ToString() != "")
                            model.amountC = Decimal.Parse(dr["amountC"].ToString());
                        if (dr["amountInCurr"].ToString() != "")
                            model.amountInCurr = Decimal.Parse(dr["amountInCurr"].ToString());
                        if (dr["idBtw"].ToString() != "")
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        if (dr["currency"].ToString() != "")
                            model.currency = dr["currency"].ToString();
                        if (dr["cost"].ToString() != "")
                            model.cost = dr["cost"].ToString();
                        if (dr["project"].ToString() != "")
                            model.project = dr["project"].ToString();
                        if (dr["isApproved"].ToString() != "")
                            model.isApproved = Boolean.Parse(dr["isApproved"].ToString());
                        if (dr["isBooked"].ToString() != "")
                            model.isBooked = Boolean.Parse(dr["isBooked"].ToString());
                        if (dr["isSent"].ToString() != "")
                            model.isSent = Boolean.Parse(dr["isSent"].ToString());
                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());
                        if (dr["namefile"].ToString() != "")
                            model.namefile = dr["namefile"].ToString();
                        if (dr["approvedUser"].ToString() != "")
                            model.approvedUser = Int32.Parse(dr["approvedUser"].ToString());
                        if (dr["createUser"].ToString() != "")
                            model.createUser = Int32.Parse(dr["createUser"].ToString());
                        if (dr["dtCreation"].ToString() != "")
                            model.dtCreation = DateTime.Parse(dr["dtCreation"].ToString());
                        if (dr["payIban"].ToString() != "")
                            model.payIban = dr["payIban"].ToString();
                        if (dr["idDocument"].ToString() != "")
                            model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                        if (dr["idOption"].ToString() != "")
                            model.idOption = Int32.Parse(dr["idOption"].ToString());
                        if (dr["amountD"].ToString() != "")
                            model.amountD = Decimal.Parse(dr["amountD"].ToString());
                        if (dr["paydays"].ToString() != "")
                            model.paydays = Int32.Parse(dr["paydays"].ToString());
                        if (dr["idTask"].ToString() != "")
                            model.idTask = Int32.Parse(dr["idTask"].ToString());
                        if (dr["isAprBook"].ToString() != "")
                            model.isAprBook = Boolean.Parse(dr["isAprBook"].ToString());
                        //================================================================
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        //================================================================

                        creditmodel.Add(model);
                    }
                    return creditmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccCreditPayModel> GetInvoicesByTaskEmployee(int employee)
        {
            DataTable dataTable = new DataTable();
            dataTable = creditpayDAO.GetInvoicesByTaskEmployee(employee);
            List<AccCreditPayModel> creditmodel = new List<AccCreditPayModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccCreditPayModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccCreditPayModel();

                        if (dr["isSelected"].ToString() != "")
                            model.isSelected = Boolean.Parse(dr["isSelected"].ToString());
                        model.idCreditPay = Int32.Parse(dr["idCreditPay"].ToString());
                        if (dr["dtItem"].ToString() != "")
                            model.dtItem = DateTime.Parse(dr["dtItem"].ToString());
                        if (dr["dtValuta"].ToString() != "")
                            model.dtValuta = DateTime.Parse(dr["dtValuta"].ToString());
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["account"].ToString() != "")
                            model.account = dr["account"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["inkopNr"].ToString() != "")
                            model.inkopNr = dr["inkopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["descItem"].ToString() != "")
                            model.descItem = dr["descItem"].ToString();
                        if (dr["amountC"].ToString() != "")
                            model.amountC = Decimal.Parse(dr["amountC"].ToString());
                        if (dr["amountInCurr"].ToString() != "")
                            model.amountInCurr = Decimal.Parse(dr["amountInCurr"].ToString());
                        if (dr["idBtw"].ToString() != "")
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        if (dr["currency"].ToString() != "")
                            model.currency = dr["currency"].ToString();
                        if (dr["cost"].ToString() != "")
                            model.cost = dr["cost"].ToString();
                        if (dr["project"].ToString() != "")
                            model.project = dr["project"].ToString();
                        if (dr["isApproved"].ToString() != "")
                            model.isApproved = Boolean.Parse(dr["isApproved"].ToString());
                        if (dr["isBooked"].ToString() != "")
                            model.isBooked = Boolean.Parse(dr["isBooked"].ToString());
                        if (dr["isSent"].ToString() != "")
                            model.isSent = Boolean.Parse(dr["isSent"].ToString());
                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());
                        if (dr["namefile"].ToString() != "")
                            model.namefile = dr["namefile"].ToString();
                        if (dr["approvedUser"].ToString() != "")
                            model.approvedUser = Int32.Parse(dr["approvedUser"].ToString());
                        if (dr["createUser"].ToString() != "")
                            model.createUser = Int32.Parse(dr["createUser"].ToString());
                        if (dr["dtCreation"].ToString() != "")
                            model.dtCreation = DateTime.Parse(dr["dtCreation"].ToString());
                        if (dr["payIban"].ToString() != "")
                            model.payIban = dr["payIban"].ToString();
                        if (dr["idDocument"].ToString() != "")
                            model.idDocument = Int32.Parse(dr["idDocument"].ToString());
                        if (dr["idOption"].ToString() != "")
                            model.idOption = Int32.Parse(dr["idOption"].ToString());
                        if (dr["amountD"].ToString() != "")
                            model.amountD = Decimal.Parse(dr["amountD"].ToString());
                        if (dr["paydays"].ToString() != "")
                            model.paydays = Int32.Parse(dr["paydays"].ToString());
                        if (dr["idTask"].ToString() != "")
                            model.idTask = Int32.Parse(dr["idTask"].ToString());
                        if (dr["isAprBook"].ToString() != "")
                            model.isAprBook = Boolean.Parse(dr["isAprBook"].ToString());
                        //================================================================
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        //================================================================

                        creditmodel.Add(model);
                    }
                    return creditmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
