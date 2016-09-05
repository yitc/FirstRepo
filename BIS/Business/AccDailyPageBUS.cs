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
    public class AccDailyPageBUS
    {
        private AccDailyPageDAO pageDAO;

        public AccDailyPageBUS()
        {
            pageDAO = new AccDailyPageDAO();
        }

        public int GetLastID()
        {
            return pageDAO.GetLastID();
        }

        public int SaveAndReturnID(AccDailyPageModel pagemodel)
        {
            int retval = 0;
            try
            {

                retval = pageDAO.SaveAndReturnID(pagemodel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateCodeDaily(string codeDaily, int id)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.UpdateCodeDaily(codeDaily, id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateRefNo(string refNo, int id)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.UpdateRefNo(refNo, id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateDtStatement(DateTime date, int id)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.UpdateDtStatement(date, id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateBegSaldo(decimal begsaldo, int id)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.UpdateBegSaldo(begsaldo, id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateEndSaldo(decimal endsaldo, int id)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.UpdateEndSaldo(endsaldo, id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool UpdateBankKas(int bankKas, int id)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.UpdateBankKas(bankKas, id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<AccDailyBankModel> GetAllByDaily(string idDaily)
        {
            DataTable dataTable = new DataTable();
            dataTable = pageDAO.GetAllByDaily(idDaily);
            List<AccDailyBankModel> linesmodel = new List<AccDailyBankModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyBankModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyBankModel();

                        model.idDailyBank = Int32.Parse(dr["idDailyBank"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();
                        if (dr["refNo"].ToString() != "")
                            model.refNo = Int32.Parse(dr["refNo"].ToString());
                        if (dr["dtStatement"].ToString() != "")
                            model.dtStatement = DateTime.Parse(dr["dtStatement"].ToString());
                        if (dr["begSaldo"].ToString() != "")
                            model.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
                        if (dr["endSaldo"].ToString() != "")
                            model.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
                        //if (dr["bankKas"].ToString() != "")
                        //    model.bankKas = Int32.Parse(dr["bankKas"].ToString());

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

        //public List<AccDailyBankModel> GetAllBanks()
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = dbankDAO.GetAllBanks();
        //    List<AccDailyBankModel> linesmodel = new List<AccDailyBankModel>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {

        //            AccDailyBankModel model = null;
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                model = new AccDailyBankModel();

        //                model.idDailyBank = Int32.Parse(dr["idDailyBank"].ToString());

        //                if (dr["codeDaily"].ToString() != "")
        //                    model.codeDaily = dr["codeDaily"].ToString();
        //                if (dr["refNo"].ToString() != "")
        //                    model.refNo = dr["refNo"].ToString();
        //                if (dr["dtStatement"].ToString() != "")
        //                    model.dtStatement = DateTime.Parse(dr["dtStatement"].ToString());
        //                if (dr["begSaldo"].ToString() != "")
        //                    model.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
        //                if (dr["endSaldo"].ToString() != "")
        //                    model.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
        //                //if (dr["bankKas"].ToString() != "")
        //                //    model.bankKas = Int32.Parse(dr["bankKas"].ToString());
        //                //if (dr["nameBankKas"].ToString() != "")
        //                //    model.nameBankKas = dr["nameBankKas"].ToString();


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

        //public List<IModel> GetAllBanksForLookup()
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = dbankDAO.GetAllBanks();
        //    List<IModel> linesmodel = new List<IModel>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {

        //            AccDailyBankModel model = null;
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                model = new AccDailyBankModel();

        //                model.idDailyBank = Int32.Parse(dr["idDailyBank"].ToString());

        //                if (dr["codeDaily"].ToString() != "")
        //                    model.codeDaily = dr["codeDaily"].ToString();
        //                if (dr["refNo"].ToString() != "")
        //                    model.refNo = dr["refNo"].ToString();
        //                if (dr["dtStatement"].ToString() != "")
        //                    model.dtStatement = DateTime.Parse(dr["dtStatement"].ToString());
        //                if (dr["begSaldo"].ToString() != "")
        //                    model.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
        //                if (dr["endSaldo"].ToString() != "")
        //                    model.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
        //                //if (dr["bankKas"].ToString() != "")
        //                //    model.bankKas = Int32.Parse(dr["bankKas"].ToString());

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

        //public AccDailyBankModel GetLastBank()
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = dbankDAO.GetLastBank();
        //    AccDailyBankModel linesmodel = new AccDailyBankModel();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {

        //            // AccDailyBankModel model = null;
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                // model = new AccDailyBankModel();

        //                linesmodel.idDailyBank = Int32.Parse(dr["idDailyBank"].ToString());

        //                if (dr["codeDaily"].ToString() != "")
        //                    linesmodel.codeDaily = dr["codeDaily"].ToString();
        //                if (dr["refNo"].ToString() != "")
        //                    linesmodel.refNo = dr["refNo"].ToString();
        //                if (dr["dtStatement"].ToString() != "")
        //                    linesmodel.dtStatement = DateTime.Parse(dr["dtStatement"].ToString());
        //                if (dr["begSaldo"].ToString() != "")
        //                    linesmodel.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
        //                if (dr["endSaldo"].ToString() != "")
        //                    linesmodel.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
        //                //if (dr["bankKas"].ToString() != "")
        //                //    linesmodel.bankKas = Int32.Parse(dr["bankKas"].ToString());

        //                // linesmodel.Add(model);
        //            }
        //            return linesmodel;

        //        }
        //        else
        //            return null;
        //    }
        //    else
        //        return null;
        //}

    }
}