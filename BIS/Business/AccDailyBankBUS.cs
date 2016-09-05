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
    public class AccDailyBankBUS
    {
        private AccDailyBankDAO dbankDAO;

        public AccDailyBankBUS(string bookyear)
        {
            dbankDAO = new AccDailyBankDAO(bookyear);
        }

        public bool Save(AccDailyBankModel dbankmodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dbankDAO.Save(dbankmodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public int SaveAndReturnID(AccDailyBankModel dbankmodel,string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = dbankDAO.SaveAndReturnID(dbankmodel,nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccDailyBankModel dbankmodel,string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = dbankDAO.Update(dbankmodel, nameForm, idUser);

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

                retval = dbankDAO.Delete(id, nameForm, idUser);

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
            dataTable = dbankDAO.GetAllByDaily(idDaily);
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
                        if (dr["difference"].ToString() != "")
                            model.difference = Decimal.Parse(dr["difference"].ToString());
                        if (dr["begSaldo"].ToString() != "")
                            model.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
                        if (dr["endSaldo"].ToString() != "")
                            model.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
                        if (dr["Booked"].ToString() != "")
                            model.booked = Decimal.Parse(dr["Booked"].ToString());
                        if (dr["pdfFile"].ToString() != "")
                            model.pdfFile = dr["pdfFile"].ToString();

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

        public AccDailyBankModel GetLastBank(string iddaily)
        {
            DataTable dataTable = new DataTable();
            dataTable = dbankDAO.GetLastBank(iddaily);
            AccDailyBankModel linesmodel = new AccDailyBankModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                   // AccDailyBankModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                       // model = new AccDailyBankModel();

                        linesmodel.idDailyBank = Int32.Parse(dr["idDailyBank"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            linesmodel.codeDaily = dr["codeDaily"].ToString();
                        if (dr["refNo"].ToString() != "")
                            linesmodel.refNo = Int32.Parse(dr["refNo"].ToString());
                        if (dr["dtStatement"].ToString() != "")
                            linesmodel.dtStatement = DateTime.Parse(dr["dtStatement"].ToString());
                        if (dr["begSaldo"].ToString() != "")
                            linesmodel.begSaldo = Decimal.Parse(dr["begSaldo"].ToString());
                        if (dr["endSaldo"].ToString() != "")
                            linesmodel.endSaldo = Decimal.Parse(dr["endSaldo"].ToString());
                        if (dr["pdfFile"].ToString() != "")
                            linesmodel.pdfFile = dr["pdfFile"].ToString();

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
    }
}