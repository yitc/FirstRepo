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
    public class AccCreditLineBUS
    {
        private AccCreditLineDAO creditlineDAO;

        public AccCreditLineBUS()
        {
            creditlineDAO = new AccCreditLineDAO();
        }

        public bool Save(AccLineModel linesmodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = creditlineDAO.Save(linesmodel, nameForm, idUser);

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

                retval = creditlineDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteDaily(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = creditlineDAO.DeleteDaily(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        //===================
        public List<AccLineModel> GetLine(int idAccLine)
        {
            DataTable dataTable = new DataTable();
            dataTable = creditlineDAO.GetLine(idAccLine);
            List<AccLineModel> alllinesmodel = new List<AccLineModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    int i = 0;
                    AccLineModel linesmodel = null;
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


                        if (dr["idMaster"].ToString() != "")
                            linesmodel.idMaster = dr["idMaster"].ToString();
                        else
                            if (dr["incopNr"].ToString() != "")
                                linesmodel.idMaster = dr["incopNr"].ToString() + (i+1).ToString();

                        if (dr["idDetail"].ToString() != "")
                            linesmodel.idDetail = dr["idDetail"].ToString();

                        if (dr["descLedgerAccount"].ToString() != "")
                            linesmodel.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        //================================================================
                        if (dr["userCreated"].ToString() != "")
                            linesmodel.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            linesmodel.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            linesmodel.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            linesmodel.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        //================================================================


                        alllinesmodel.Add(linesmodel);
                        i++;
                    }
                    return alllinesmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        //provera
    }
}