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
    public class AccLedgerAmountsBUS
    {

        private AccLedgerAmountsDAO amountsDAO;

        public AccLedgerAmountsBUS()
        {
            amountsDAO = new AccLedgerAmountsDAO();
        }

        public bool Delete(string konto, string year, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = amountsDAO.Delete(konto, year, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<AccLedgerAmountsModel> GetAllAmounts(string year)
        {
            DataTable dataTable = new DataTable();
            dataTable = amountsDAO.GetAllAmounts(year);
            List<AccLedgerAmountsModel> lmodel = new List<AccLedgerAmountsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccLedgerAmountsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLedgerAmountsModel();
                        if (dr["idAccount"].ToString() != "")
                            model.idAccount = Int32.Parse(dr["idAccount"].ToString());

                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["beginDebit"].ToString() != "")
                            model.beginDebit = Decimal.Parse(dr["beginDebit"].ToString());
                        if (dr["beginCredit"].ToString() != "")
                            model.beginCredit = Decimal.Parse(dr["beginCredit"].ToString());
                        if (dr["debitAmount"].ToString() != "")
                            model.debitAmount = Decimal.Parse(dr["debitAmount"].ToString());
                        if (dr["creditAmount"].ToString() != "")
                            model.creditAmount = Decimal.Parse(dr["creditAmount"].ToString());
                        if (dr["transactionsNo"].ToString() != "")
                            model.transactionsNo = Int32.Parse(dr["transactionsNo"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        lmodel.Add(model);
                    }
                    return lmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccLedgerAmountsModel GetAmountPerYear(string konto, string year)
        {
            DataTable dataTable = new DataTable();
            dataTable = amountsDAO.GetAmountPerYear(konto, year);
            AccLedgerAmountsModel lmodel = new AccLedgerAmountsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccLedgerAmountsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLedgerAmountsModel();
                        if (dr["idAccount"].ToString() != "")
                            model.idAccount = Int32.Parse(dr["idAccount"].ToString());

                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["beginDebit"].ToString() != "")
                            model.beginDebit = Decimal.Parse(dr["beginDebit"].ToString());
                        if (dr["beginCredit"].ToString() != "")
                            model.beginCredit = Decimal.Parse(dr["beginCredit"].ToString());
                        if (dr["debitAmount"].ToString() != "")
                            model.debitAmount = Decimal.Parse(dr["debitAmount"].ToString());
                        if (dr["creditAmount"].ToString() != "")
                            model.creditAmount = Decimal.Parse(dr["creditAmount"].ToString());
                        if (dr["transactionsNo"].ToString() != "")
                            model.transactionsNo = Int32.Parse(dr["transactionsNo"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                       // lmodel.Add(model);
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
                
        }

        public bool Save(AccLedgerAmountsModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = amountsDAO.Save(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccLedgerAmountsModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = amountsDAO.Update(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

     
            
      
    }
}
