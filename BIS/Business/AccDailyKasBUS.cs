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
    public class AccDailyKasBUS
    {
        private AccDailyKasDAO accDailyKasDAO;

        public AccDailyKasBUS()
        {
            accDailyKasDAO = new AccDailyKasDAO();
        }


        public bool Delete(int id, string nameForm, int idUser,string bookYear)
        {
            bool retval = false;
            try
            {

                retval = accDailyKasDAO.Delete(id, nameForm, idUser, bookYear);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public int SaveAndReturnID(AccDailyKasModel accKas, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = accDailyKasDAO.SaveAndReturnID(accKas, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<AccDailyKasModel>GetAllKas()
        {
            DataTable dataTable = new DataTable();
            dataTable = accDailyKasDAO.GetAllAccDailyKas();

            List<AccDailyKasModel> accDailyKasModel = new List<AccDailyKasModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccDailyKasModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyKasModel();  //AccDailyKasModel

                        model.idAccDailyKas = Int32.Parse(dr["idAccDailyKas"].ToString());
                        model.codeDaily = dr["codeDaily"].ToString();
                        model.refnoKas = Int32.Parse(dr["refnoKas"].ToString());

                        if (dr["dtKas"].ToString() != "")
                        {
                            model.dtKas = Convert.ToDateTime(dr["dtKas"].ToString());
                        }
                        if (dr["begSaldo"].ToString() != "")
                            model.begSaldo = Convert.ToDecimal(dr["begSaldo"].ToString());
                        if (dr["endSaldo"].ToString() != "")
                            model.endSaldo = Convert.ToDecimal(dr["endSaldo"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                       
                        accDailyKasModel.Add(model);

                    }
                    return accDailyKasModel;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccDailyKasModel> GetAllAccDailyKas()
        {
            List<AccDailyKasModel> compList = new List<AccDailyKasModel>();

            DataTable dataTable = new DataTable();
            dataTable = accDailyKasDAO.GetAllAccDailyKas();

            if(dataTable !=null)
            {
                foreach(DataRow dr in dataTable.Rows)
                {
                    AccDailyKasModel model = new AccDailyKasModel();

                    model.idAccDailyKas = Int32.Parse(dr["idAccDailyKas"].ToString());
                    model.codeDaily = dr["codeDaily"].ToString();
                    model.refnoKas = Int32.Parse(dr["refnoKas"].ToString());

                    if (dr["dtKas"].ToString() !="")
                    { 
                    model.dtKas = Convert.ToDateTime(dr["dtKas"].ToString());
                    }
                    if (dr["begSaldo"].ToString() != "")
                        model.begSaldo = Convert.ToDecimal(dr["begSaldo"].ToString());
                    if (dr["endSaldo"].ToString() != "")
                        model.endSaldo = Convert.ToDecimal(dr["endSaldo"].ToString());
                    if (dr["bookingYear"].ToString() != "")
                        model.bookingYear = dr["bookingYear"].ToString();
                    if (dr["userCreated"].ToString() != "")
                        model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                    if (dr["dtCreated"].ToString() != "")
                        model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                    if (dr["userModified"].ToString() != "")
                        model.userModified = Int32.Parse(dr["userModified"].ToString());
                    if (dr["dtModified"].ToString() != "")
                        model.dtModified = DateTime.Parse(dr["dtModified"].ToString());


                    compList.Add(model);
                }
                return compList;
            }
            else
            {
                return null;
            }
        }

        public AccDailyKasModel GetAccDailyKasById(string idAccDailyKas)
        {
            DataTable dataTable = new DataTable();
            dataTable = accDailyKasDAO.GetAllAccDailyKasByID(idAccDailyKas);

            AccDailyKasModel accDailyKas = new AccDailyKasModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccDailyKasModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyKasModel();

                        model.idAccDailyKas = Int32.Parse(dr["idAccDailyKas"].ToString());
                        model.codeDaily = dr["codeDaily"].ToString();
                        model.refnoKas = Int32.Parse(dr["refnoKas"].ToString());

                        if (dr["dtKas"].ToString() != "")
                        {
                            model.dtKas = Convert.ToDateTime(dr["dtKas"].ToString());
                        }
                        if (dr["begSaldo"].ToString() != "")
                            model.begSaldo = Convert.ToDecimal(dr["begSaldo"].ToString());
                        if (dr["endSaldo"].ToString() != "")
                            model.endSaldo = Convert.ToDecimal(dr["endSaldo"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public AccDailyKasModel GetLastKas()
        {
            DataTable dataTable = new DataTable();
            dataTable = accDailyKasDAO.GetLastKas();
            AccDailyKasModel linesmodel = new AccDailyKasModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    // AccDailyBankModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new AccDailyBankModel();

                        linesmodel.idAccDailyKas = Int32.Parse(dr["idAccDailyKas"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            linesmodel.codeDaily = dr["codeDaily"].ToString();
                        if (dr["refnoKas"].ToString() != "")
                            linesmodel.refnoKas = Int32.Parse(dr["refnoKas"].ToString());
                        if (dr["dtKas"].ToString() != "")
                            linesmodel.dtKas = DateTime.Parse(dr["dtKas"].ToString());
                        if (dr["begSaldo"].ToString() != "")
                            linesmodel.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
                        if (dr["endSaldo"].ToString() != "")
                            linesmodel.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            linesmodel.bookingYear = dr["bookingYear"].ToString();
                        if (dr["userCreated"].ToString() != "")
                            linesmodel.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            linesmodel.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            linesmodel.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            linesmodel.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                       
                        //if (dr["bankKas"].ToString() != "")
                        //    linesmodel.bankKas = Int32.Parse(dr["bankKas"].ToString());

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
        public List<AccDailyKasModel> GetAllByDaily(string idDaily,string bookYear)
        {
            DataTable dataTable = new DataTable();
            dataTable = accDailyKasDAO.GetAllByDaily(idDaily, bookYear);
            List<AccDailyKasModel> linesmodel = new List<AccDailyKasModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyKasModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyKasModel();

                        model.idAccDailyKas = Int32.Parse(dr["idAccDailyKas"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["refnoKas"].ToString() != "")
                            model.refnoKas = Int32.Parse(dr["refnoKas"].ToString());
                        if (dr["dtKas"].ToString() != "")
                            model.dtKas = DateTime.Parse(dr["dtKas"].ToString());
                        if (dr["difference"].ToString() != "")
                            model.difference = Decimal.Parse(dr["difference"].ToString());
                        if (dr["begSaldo"].ToString() != "")
                            model.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
                        if (dr["endSaldo"].ToString() != "")
                            model.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
                        if (dr["Booked"].ToString() != "")
                            model.booked = Decimal.Parse(dr["Booked"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            model.bookingYear = dr["bookingYear"].ToString();

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
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
    }
    
}