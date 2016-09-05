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
    public class AccDailyBUS
    {
        private AccDailyDAO dailyDAO;

        public AccDailyBUS(string bookyear)
        {
            dailyDAO = new AccDailyDAO(bookyear);
        }

        public bool Save(AccDailyModel dailymodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dailyDAO.Save(dailymodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public bool Update(AccDailyModel dailymodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dailyDAO.Update(dailymodel, nameForm, idUser);

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

                retval = dailyDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetAllDailys()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetAllDailys();
            List<IModel> dailymodel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                   
                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();

                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                           model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            model.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                       // model.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            model.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();

                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                      dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<IModel> GetBookingDailys()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetBookingDailys();
            List<IModel> dailymodel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();

                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            model.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                        // model.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            model.unBooked = Int32.Parse(dr["unBooked"].ToString());

                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<IModel> GetBookingDailysInkop()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetBookingDailysInkop();
            List<IModel> dailymodel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();

                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            model.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                        // model.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            model.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetBookingDailysMemo()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetBookingDailysMemo();
            List<IModel> dailymodel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();

                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            model.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                        // model.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            model.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());


                        dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccDailyModel> GetBookingDailysInkop2()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetBookingDailysInkop();
            List<AccDailyModel> dailymodel = new List<AccDailyModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();

                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            model.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                        // model.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            model.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());


                        dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccDailyModel GetDailysById(int idDaily)
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetDailyById(idDaily);
            AccDailyModel dailymodel = new AccDailyModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                  //  AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                     //   model = new AccDailyModel();

                        dailymodel.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            dailymodel.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            dailymodel.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            dailymodel.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            dailymodel.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            dailymodel.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            dailymodel.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            dailymodel.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            dailymodel.ibanBank = dr["ibanBank"].ToString();
                     //   dailymodel.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    dailymodel.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    dailymodel.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            dailymodel.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            dailymodel.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            dailymodel.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            dailymodel.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            dailymodel.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            dailymodel.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            dailymodel.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            dailymodel.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            dailymodel.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            dailymodel.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        //dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public AccDailyModel GetDailysByCode(string code)
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetDailyByCode(code);
            AccDailyModel dailymodel = new AccDailyModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    //  AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //   model = new AccDailyModel();

                        dailymodel.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            dailymodel.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            dailymodel.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            dailymodel.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            dailymodel.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            dailymodel.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            dailymodel.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            dailymodel.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            dailymodel.ibanBank = dr["ibanBank"].ToString();
                    //    dailymodel.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    dailymodel.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    dailymodel.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            dailymodel.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            dailymodel.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            dailymodel.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            dailymodel.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            dailymodel.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            dailymodel.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            dailymodel.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            dailymodel.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            dailymodel.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            dailymodel.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        //dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccDailyModel GetDailyByIban(string iban)
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetDailyByIban(iban);
            AccDailyModel dailymodel = new AccDailyModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    //  AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //   model = new AccDailyModel();

                        dailymodel.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            dailymodel.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            dailymodel.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            dailymodel.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            dailymodel.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            dailymodel.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            dailymodel.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            dailymodel.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            dailymodel.ibanBank = dr["ibanBank"].ToString();
                        //   dailymodel.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    dailymodel.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    dailymodel.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            dailymodel.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            dailymodel.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            dailymodel.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            dailymodel.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            dailymodel.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            dailymodel.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            dailymodel.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            dailymodel.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            dailymodel.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            dailymodel.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        //dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetMemoBeginPeriod()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetMemoBeginPeriod();
            List<IModel> dailymodel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();
                                              
                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                       
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                      
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                       
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                                         
                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccDailyModel> GetAllDailysList()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetAllDailys();
            List<AccDailyModel> dailymodel = new List<AccDailyModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();

                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            model.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        if (dr["descLedgerAccount"].ToString() != "")
                            model.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                        // model.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        if (dr["unBooked"].ToString() != "")
                            model.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();

                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AccDailyModel> GetBeginDaily()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyDAO.GetBeginDaily();
            List<AccDailyModel> dailymodel = new List<AccDailyModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyModel();

                        model.idDaily = Int32.Parse(dr["idDaily"].ToString());
                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["descDaily"].ToString() != "")
                            model.descDaily = dr["descDaily"].ToString();
                        if (dr["idDailyType"].ToString() != "")
                            model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                     //   if (dr["descDailyType"].ToString() != "")
                     //       model.descDailyType = dr["descDailyType"].ToString();
                        if (dr["numberLedgerAccount"].ToString() != "")
                            model.numberLedgerAccount = dr["numberLedgerAccount"].ToString();
                        //if (dr["descLedgerAccount"].ToString() != "")
                        //    model.descLedgerAccount = dr["descLedgerAccount"].ToString();
                        if (dr["idBank"].ToString() != "")
                            model.idBank = Int32.Parse(dr["idBank"].ToString());
                        //if (dr["nameBank"].ToString() != "")
                        //    model.nameBank = dr["nameBank"].ToString();
                        if (dr["ibanBank"].ToString() != "")
                            model.ibanBank = dr["ibanBank"].ToString();
                        // model.isLocked = Boolean.Parse(dr["isLocked"].ToString());
                        //if (dr["idDailyVerIn"].ToString() != "")
                        //    model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        //if (dr["nameDailyVerIn"].ToString() != "")
                        //    model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                      //  if (dr["unBooked"].ToString() != "")
                      //      model.unBooked = Int32.Parse(dr["unBooked"].ToString());
                        if (dr["isUseCounter"].ToString() != "")
                            model.isUseCounter = Boolean.Parse(dr["isUseCounter"].ToString());
                        if (dr["inkop"].ToString() != "")
                            model.inkop = Int32.Parse(dr["inkop"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();

                        if (dr["automaticBook"].ToString() != "")
                            model.automaticBook = Boolean.Parse(dr["automaticBook"].ToString());
                        if (dr["beginPeriod"].ToString() != "")
                            model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        dailymodel.Add(model);
                    }
                    return dailymodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}