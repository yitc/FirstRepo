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
    public class AccLineBUS
    {
        private AccLineDAO lineDAO;
        //private string bookYear;

        public AccLineBUS(string bookyear)
        {
            lineDAO = new AccLineDAO(bookyear);
        }

        public bool Save(AccLineModel linesmodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = lineDAO.Save(linesmodel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveTransact(List<AccLineModel> linesmodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
               
                retval = lineDAO.SaveTransact(linesmodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccLineModel linesmodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = lineDAO.Update(linesmodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool UpdateStatus(AccLineModel linesmodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = lineDAO.UpdateStatus(linesmodel, nameForm, idUser);

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

                retval = lineDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool DeleteByCurrencyID(int idCurrency, int idDaily, int openclose, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = lineDAO.DeleteByCurrencyID(idCurrency, idDaily, openclose, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        //===========
        public bool DeleteByReference(string incopNr, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = lineDAO.DeleteByReference(incopNr, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool DeleteByReferenceALL(string incopNr, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = lineDAO.DeleteByReferenceALL(incopNr, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        //============
        public bool MakeCounter(string year, int idDaily, int idNumber)
        {
            bool retval = false;
            try
            {

                retval = lineDAO.MakeCounter(year, idDaily,idNumber);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        //===================
        public List<AccLineModel> GetAllLinesByDaily(int idDaily, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByDaily(idDaily, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily =Int32.Parse(dr["idAccDaily"].ToString());
                      
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Convert.ToInt32(dr["userCreated"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Convert.ToInt32(dr["userModified"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());


                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> GetLinesByAccount(string account, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLinesByAccount(account, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> GetLinesByOnlyAccount(string account)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLinesByOnlyAccount(account);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccLineModel> GetAllLinesByIdCurrency(int idCurrency, int idDaily, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByIdCurrency(idCurrency,idDaily, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["Versil"].ToString() != "")
                            model.versil = Decimal.Parse(dr["Versil"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                   
                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccLineModel GetLine(int idAccLine)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLine(idAccLine);
            AccLineModel linesmodel = new AccLineModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                   // AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                         linesmodel = new AccLineModel();

                        linesmodel.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            linesmodel.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        linesmodel.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            linesmodel.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            linesmodel.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            linesmodel.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            linesmodel.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            linesmodel.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            linesmodel.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            linesmodel.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            linesmodel.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            linesmodel.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            linesmodel.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            linesmodel.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            linesmodel.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            linesmodel.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            linesmodel.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            linesmodel.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            linesmodel.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            linesmodel.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            linesmodel.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            linesmodel.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            linesmodel.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            linesmodel.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            linesmodel.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            linesmodel.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            linesmodel.idSepa = Int32.Parse(dr["idSepa"].ToString());
                        if (dr["versil"].ToString() != "")
                            linesmodel.versil = Decimal.Parse(dr["versil"].ToString());
                       // linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccLineModel> GetAllLinesByinvoice(string invoice, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByinvoice(invoice, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> GetAllLinesByNumber(string invoice, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByNumber(invoice, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    int j = 0;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());
                        if (dr["idMaster"].ToString() != "")
                            model.idMaster = dr["idMaster"].ToString();
                        else
                        {
                            if (dr["incopNr"].ToString() != "")
                                model.idMaster = dr["incopNr"].ToString()+j.ToString();
                        }
                        if (dr["idDetail"].ToString() != "")
                            model.idDetail = dr["idDetail"].ToString();

                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();

                        linesmodel.Add(model);
                        j++;
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> GetAllLinesByNumberByIncop(string invoice, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByNumberByIncop(invoice, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    int j = 0;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());
                        if (dr["idMaster"].ToString() != "")
                            model.idMaster = dr["idMaster"].ToString();
                        else
                        {
                            if (dr["incopNr"].ToString() != "")
                                model.idMaster = dr["incopNr"].ToString() + j.ToString();
                        }
                        if (dr["idDetail"].ToString() != "")
                            model.idDetail = dr["idDetail"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();

                        linesmodel.Add(model);
                        j++;
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> GetAllLinesByNumber1610(string invoice, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByNumberand1610(invoice, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        // sve stavke sa incop brojem
        public List<AccLineModel> GetAllLinesByNumberALL(string invoice, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByNumberALL(invoice, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> GetAllLinesByinvoiceAndIdDaily(string invoice, int idAccDaily, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByinvoiceAndIdDaily(invoice, idAccDaily, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<IdModel> GetAllCounters(string yearId)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllCounters(yearId);
            List<IdModel> linesmodel = new List<IdModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    IdModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new IdModel();

                        model.yearId = dr["yearId"].ToString();
                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        model.idNumber = Int32.Parse(dr["idNumber"].ToString());

                        linesmodel.Add(model);

                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public IdModel GetIncop(string yearId, int idDaily, string nameForm, int idUser)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetIncop(yearId, idDaily, nameForm, idUser);
            IdModel linesmodel = new IdModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                                       
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        linesmodel = new IdModel();

                        linesmodel.yearId = dr["yearId"].ToString();
                        linesmodel.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        linesmodel.idNumber = Int32.Parse(dr["idNumber"].ToString());
   
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public IdModel GetIncopView(string yearId, int idDaily)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetIncopView(yearId, idDaily);
            IdModel linesmodel = new IdModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        linesmodel = new IdModel();

                        linesmodel.yearId = dr["yearId"].ToString();
                        linesmodel.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        linesmodel.idNumber = Int32.Parse(dr["idNumber"].ToString());

                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public IdModel GetBankNr()
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetBankNr();
            IdModel linesmodel = new IdModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        linesmodel = new IdModel();

                        linesmodel.idNumber= Int32.Parse(dr["idNumberBank"].ToString());

                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public IdModel GetVerkopNr()
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetVerkopNr();
            IdModel linesmodel = new IdModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        linesmodel = new IdModel();

                        linesmodel.idNumber = Int32.Parse(dr["idNumVerkop"].ToString());

                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> GetLinesByAccountAndCustomer(string account, int openclose, string customer)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLinesByAccountAndCustomer(account, openclose, customer);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccLineModel> GetLinesByAccountAndCustomerAndProject(string account, string customer, string project)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLinesByAccountAndCustomerAndProject(account, customer, project);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());
                       

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineModel> CheckLines(string account, string customer)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.CheckLines(account, customer);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        //provera
        public List<AccLineModel>GetCostLineFromAccLine(string idCostLine)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetCostLineFromAccLine(idCostLine);
            List<AccLineModel> arrange = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccLineModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();
                        if (dr["idCostLine"].ToString() != "")
                        {
                            model.idCostLine = dr["idCostLine"].ToString();
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

        public List<AccLineModel> GetLinesByBTW(int idBtw)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLinesByBTW(idBtw);
            List<AccLineModel> arrange = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccLineModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();
                        if (dr["idBTW"].ToString() != "")
                        {
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
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

        public List<AccLineModel> GetLinesJournal(string daily,string account, string customer, DateTime fromdate, DateTime todate, string fromperiod, string toperiod,string cost,string project, int ord, string factur)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLinesJournal(daily, account, customer, fromdate, todate, fromperiod, toperiod, cost,project,ord, factur);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccLineModel> GetLinesOPEN(string daily, DateTime fromdate, DateTime todate, string fromperiod, string toperiod, string invoiceNr)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetLinesOPEN(daily, fromdate, todate, fromperiod, toperiod, invoiceNr);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());

                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        //public List<AccLineModel> GetLinesOPEN(string daily,  DateTime fromdate, DateTime todate, string fromperiod, string toperiod)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = lineDAO.GetLinesOPEN(daily, fromdate, todate, fromperiod, toperiod);
        //    List<AccLineModel> linesmodel = new List<AccLineModel>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {

        //            AccLineModel model = null;
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                model = new AccLineModel();

        //                model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
        //                if (dr["idAccDaily"].ToString() != "")
        //                    model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
        //                model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
        //                if (dr["periodLine"].ToString() != "")
        //                    model.periodLine = Int32.Parse(dr["periodLine"].ToString());
        //                if (dr["dtLine"].ToString() != "")
        //                    model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
        //                if (dr["numberLedAccount"].ToString() != "")
        //                    model.numberLedAccount = dr["numberLedAccount"].ToString();
        //                if (dr["invoiceNr"].ToString() != "")
        //                    model.invoiceNr = dr["invoiceNr"].ToString();
        //                if (dr["descLine"].ToString() != "")
        //                    model.descLine = dr["descLine"].ToString();
        //                if (dr["idClientLine"].ToString() != "")
        //                    model.idClientLine = dr["idClientLine"].ToString();
        //                if (dr["idPersonLine"].ToString() != "")
        //                    model.idPersonLine = dr["idPersonLine"].ToString();
        //                if (dr["idCostLine"].ToString() != "")
        //                    model.idCostLine = dr["idCostLine"].ToString();
        //                if (dr["idProjectLine"].ToString() != "")
        //                    model.idProjectLine = dr["idProjectLine"].ToString();
        //                if (dr["debitLine"].ToString() != "")
        //                    model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
        //                if (dr["creditLine"].ToString() != "")
        //                    model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
        //                if (dr["idBTW"].ToString() != "")
        //                    model.idBTW = Int32.Parse(dr["idBTW"].ToString());
        //                if (dr["debitBTW"].ToString() != "")
        //                    model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
        //                if (dr["creditBTW"].ToString() != "")
        //                    model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
        //                if (dr["idCurrency"].ToString() != "")
        //                    model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
        //                if (dr["debitCurr"].ToString() != "")
        //                    model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
        //                if (dr["creditCurr"].ToString() != "")
        //                    model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
        //                if (dr["dtBooking"].ToString() != "")
        //                    model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
        //                if (dr["booksort"].ToString() != "")
        //                    model.booksort = Int32.Parse(dr["booksort"].ToString());
        //                if (dr["currrate"].ToString() != "")
        //                    model.currrate = Decimal.Parse(dr["currrate"].ToString());
        //                if (dr["incopNr"].ToString() != "")
        //                    model.incopNr = dr["incopNr"].ToString();
        //                if (dr["bookingYear"].ToString() != "")
        //                    model.bookingYear = dr["bookingYear"].ToString();
        //                if (dr["term"].ToString() != "")
        //                    model.term = Int32.Parse(dr["term"].ToString());
        //                if (dr["idSepa"].ToString() != "")
        //                    model.idSepa = Int32.Parse(dr["idSepa"].ToString());

        //                linesmodel.Add(model);
        //            }
        //            return linesmodel;

        //        }
        //        else
        //            return null;
        //    }
        //    else
        //        return null;
        //}
        public List<AccLineModel> GetAllLinesYear(string year)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesYear(year);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());

                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());


                        linesmodel.Add(model);
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccLineModel> GetAllLinesByNumberAutomatic(string invoice, int openclose)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetAllLinesByNumberAutomatic(invoice, openclose);
            List<AccLineModel> linesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineModel model = null;
                    int j = 0;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineModel();

                        model.idAccLine = Int32.Parse(dr["idAccLine"].ToString());
                        if (dr["idAccDaily"].ToString() != "")
                            model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());
                        model.statusLine = Boolean.Parse(dr["statusLine"].ToString());
                        if (dr["periodLine"].ToString() != "")
                            model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                        if (dr["dtLine"].ToString() != "")
                            model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["descLine"].ToString() != "")
                            model.descLine = dr["descLine"].ToString();
                        if (dr["idClientLine"].ToString() != "")
                            model.idClientLine = dr["idClientLine"].ToString();
                        if (dr["idPersonLine"].ToString() != "")
                            model.idPersonLine = dr["idPersonLine"].ToString();
                        if (dr["idCostLine"].ToString() != "")
                            model.idCostLine = dr["idCostLine"].ToString();
                        if (dr["idProjectLine"].ToString() != "")
                            model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debitLine"].ToString() != "")
                            model.debitLine = Decimal.Parse(dr["debitLine"].ToString());
                        if (dr["creditLine"].ToString() != "")
                            model.creditLine = Decimal.Parse(dr["creditLine"].ToString());
                        if (dr["idBTW"].ToString() != "")
                            model.idBTW = Int32.Parse(dr["idBTW"].ToString());
                        if (dr["debitBTW"].ToString() != "")
                            model.debitBTW = Decimal.Parse(dr["debitBTW"].ToString());
                        if (dr["creditBTW"].ToString() != "")
                            model.creditBTW = Decimal.Parse(dr["creditBTW"].ToString());
                        if (dr["idCurrency"].ToString() != "")
                            model.idCurrency = Int32.Parse(dr["idCurrency"].ToString());
                        if (dr["debitCurr"].ToString() != "")
                            model.debitCurr = Decimal.Parse(dr["debitCurr"].ToString());
                        if (dr["creditCurr"].ToString() != "")
                            model.creditCurr = Decimal.Parse(dr["creditCurr"].ToString());
                        if (dr["dtBooking"].ToString() != "")
                            model.dtBooking = DateTime.Parse(dr["dtBooking"].ToString());
                        if (dr["booksort"].ToString() != "")
                            model.booksort = Int32.Parse(dr["booksort"].ToString());
                        if (dr["currrate"].ToString() != "")
                            model.currrate = Decimal.Parse(dr["currrate"].ToString());
                        if (dr["incopNr"].ToString() != "")
                            model.incopNr = dr["incopNr"].ToString();
                        if (dr["iban"].ToString() != "")
                            model.iban = dr["iban"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["idSepa"].ToString() != "")
                            model.idSepa = Int32.Parse(dr["idSepa"].ToString());
                        if (dr["idMaster"].ToString() != "")
                            model.idMaster = dr["idMaster"].ToString();
                        else
                        {
                            if (dr["incopNr"].ToString() != "")
                                model.idMaster = dr["incopNr"].ToString() + j.ToString();
                        }
                        if (dr["idDetail"].ToString() != "")
                            model.idDetail = dr["idDetail"].ToString();

                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();

                        linesmodel.Add(model);
                        j++;
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineBeginModel> GetBeginAmounts(string bookingYear, string creditor, string debitor)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetBeginAmounts(bookingYear, creditor, debitor);
            List<AccLineBeginModel> linesmodel = new List<AccLineBeginModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineBeginModel model = null;
                   
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineBeginModel();

                       
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                       // if (dr["descLine"].ToString() != "")
                       //     model.descLine = dr["descLine"].ToString();
                       // if (dr["idClientLine"].ToString() != "")
                       //     model.idClientLine = dr["idClientLine"].ToString();
                        //if (dr["idCostLine"].ToString() != "")
                        //    model.idCostLine = dr["idCostLine"].ToString();
                        //if (dr["idProjectLine"].ToString() != "")
                        //    model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debit"].ToString() != "")
                            model.debit = Decimal.Parse(dr["debit"].ToString());
                        if (dr["credit"].ToString() != "")
                            model.credit = Decimal.Parse(dr["credit"].ToString());
                        if (dr["diff"].ToString() != "")
                            model.diff = Decimal.Parse(dr["diff"].ToString());
                       

                        linesmodel.Add(model);
                      
                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineBeginModel> GetBeginSUM4Amounts(string bookingYear)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetBeginSUM4Amounts(bookingYear);
            List<AccLineBeginModel> linesmodel = new List<AccLineBeginModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineBeginModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineBeginModel();


                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        // if (dr["descLine"].ToString() != "")
                        //     model.descLine = dr["descLine"].ToString();
                        //if (dr["idClientLine"].ToString() != "")
                        //    model.idClientLine = dr["idClientLine"].ToString();
                        //if (dr["idCostLine"].ToString() != "")
                        //    model.idCostLine = dr["idCostLine"].ToString();
                        //if (dr["idProjectLine"].ToString() != "")
                        //    model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debit"].ToString() != "")
                            model.debit = Decimal.Parse(dr["debit"].ToString());
                        if (dr["credit"].ToString() != "")
                            model.credit = Decimal.Parse(dr["credit"].ToString());
                        if (dr["diff"].ToString() != "")
                            model.diff = Decimal.Parse(dr["diff"].ToString());


                        linesmodel.Add(model);

                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccLineBeginModel> GetBeginSUM8Amounts(string bookingYear)
        {
            DataTable dataTable = new DataTable();
            dataTable = lineDAO.GetBeginSUM8Amounts(bookingYear);
            List<AccLineBeginModel> linesmodel = new List<AccLineBeginModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccLineBeginModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccLineBeginModel();


                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        // if (dr["descLine"].ToString() != "")
                        //     model.descLine = dr["descLine"].ToString();
                        //if (dr["idClientLine"].ToString() != "")
                        //    model.idClientLine = dr["idClientLine"].ToString();
                        //if (dr["idCostLine"].ToString() != "")
                        //    model.idCostLine = dr["idCostLine"].ToString();
                        //if (dr["idProjectLine"].ToString() != "")
                        //    model.idProjectLine = dr["idProjectLine"].ToString();
                        if (dr["debit"].ToString() != "")
                            model.debit = Decimal.Parse(dr["debit"].ToString());
                        if (dr["credit"].ToString() != "")
                            model.credit = Decimal.Parse(dr["credit"].ToString());
                        if (dr["diff"].ToString() != "")
                            model.diff = Decimal.Parse(dr["diff"].ToString());


                        linesmodel.Add(model);

                    }
                    return linesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}