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
    public class AccDailyMemBUS
    {

        private AccDailyMemDAO memDAO;

        public AccDailyMemBUS(string bookyear)
        {
            memDAO = new AccDailyMemDAO(bookyear);
        }



         public AccDailyMemModel GetMemosById(int id)
         {
             DataTable dataTable = new DataTable();
             dataTable = memDAO.GetMemoById(id);
           //  List<AccDailyMemModel> lmodel = new List<AccDailyMemModel>();

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {
                     AccDailyMemModel model = null;
                     foreach (DataRow dr in dataTable.Rows)
                     {
                         model = new AccDailyMemModel();
                         model.idDailyMem = Int32.Parse(dr["idDailyMem"].ToString());

                         if (dr["codeDaily"].ToString() != "")
                             model.codeDaily = dr["codeDaily"].ToString();
                         if (dr["refno"].ToString() != "")
                             model.refNo = Int32.Parse(dr["refno"].ToString());
                         if (dr["dtMem"].ToString() != "")
                             model.dtMem = DateTime.Parse(dr["dtMem"].ToString());
                         if (dr["beginPeriod"].ToString() != "")
                             model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());
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

                      //   lmodel.Add(model);
                     }
                     return model;

                 }
                 else
                     return null;
             }
             else
                 return null;
         }

         public List<AccDailyMemModel> GetMemoByIdWithDebitCredit(int id)
         {
             DataTable dataTable = new DataTable();
             dataTable = memDAO.GetMemoByIdWithDebitCredit(id);
             List<AccDailyMemModel> lmodel = new List<AccDailyMemModel>();

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {
                     AccDailyMemModel model = null;
                     foreach (DataRow dr in dataTable.Rows)
                     {
                         model = new AccDailyMemModel();
                         model.idDailyMem = Int32.Parse(dr["idDailyMem"].ToString());

                         if (dr["codeDaily"].ToString() != "")
                             model.codeDaily = dr["codeDaily"].ToString();
                         if (dr["refno"].ToString() != "")
                             model.refNo = Int32.Parse(dr["refno"].ToString());
                         if (dr["dtMem"].ToString() != "")
                             model.dtMem = DateTime.Parse(dr["dtMem"].ToString());
                         if (dr["beginPeriod"].ToString() != "")
                             model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());
                         if (dr["bookingYear"].ToString() != "")
                             model.bookingYear = dr["bookingYear"].ToString();

                         if (dr["debit"].ToString() != "")
                             model.debit = Decimal.Parse(dr["debit"].ToString());

                         if (dr["credit"].ToString() != "")
                             model.credit = Decimal.Parse(dr["credit"].ToString());

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

         public List<AccDailyMemModel> GetAllByDaily(string idDaily)
         {
             DataTable dataTable = new DataTable();
             dataTable = memDAO.GetAllByDaily(idDaily);
             List<AccDailyMemModel> linesmodel = new List<AccDailyMemModel>();

             if (dataTable != null)
             {
                 if (dataTable.Rows.Count > 0)
                 {

                     AccDailyMemModel model = null;
                     foreach (DataRow dr in dataTable.Rows)
                     {
                         model = new AccDailyMemModel();

                         model.idDailyMem = Int32.Parse(dr["idDailyMem"].ToString());

                         if (dr["codeDaily"].ToString() != "")
                             model.codeDaily = dr["codeDaily"].ToString();
                         if (dr["refNo"].ToString() != "")
                             model.refNo = Int32.Parse(dr["refNo"].ToString());
                         if (dr["dtMem"].ToString() != "")
                             model.dtMem = DateTime.Parse(dr["dtMem"].ToString());
                         if (dr["beginPeriod"].ToString() != "")
                             model.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());
                         if (dr["bookingYear"].ToString() != "")
                             model.bookingYear = dr["bookingYear"].ToString();
                         if (dr["debit"].ToString() != "")
                             model.debit = Decimal.Parse(dr["debit"].ToString());
                         if (dr["credit"].ToString() != "")
                             model.credit = Decimal.Parse(dr["credit"].ToString());

                         if (dr["userCreated"].ToString() != "")
                             model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                         if (dr["dtCreated"].ToString() != "")
                             model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                         if (dr["userModified"].ToString() != "")
                             model.userModified = Int32.Parse(dr["userModified"].ToString());
                         if (dr["dtModified"].ToString() != "")
                             model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        
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

         public int SaveAndReturnID(AccDailyMemModel dbankmodel, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = memDAO.SaveAndReturnID(dbankmodel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
         public bool Update(AccDailyMemModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = memDAO.Update(model, nameForm, idUser);

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

                retval = memDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete2(int id, int refno, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = memDAO.Delete2(id, refno, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public AccDailyMemModel GetLastMemByStatement(string code)
        {
            DataTable dataTable = new DataTable();
            dataTable = memDAO.GetLastMemByStatement(code);
            AccDailyMemModel linesmodel = new AccDailyMemModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    // AccDailyBankModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        // model = new AccDailyBankModel();

                        linesmodel.idDailyMem = Int32.Parse(dr["idDailyMem"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            linesmodel.codeDaily = dr["codeDaily"].ToString();
                        if (dr["refNo"].ToString() != "")
                            linesmodel.refNo = Int32.Parse(dr["refNo"].ToString());
                        if (dr["dtMem"].ToString() != "")
                            linesmodel.dtMem = DateTime.Parse(dr["dtMem"].ToString());
                        if (dr["bookingYear"].ToString() != "")
                            linesmodel.bookingYear = dr["bookingYear"].ToString();
                        if (dr["beginPeriod"].ToString() != "")
                            linesmodel.beginPeriod = Boolean.Parse(dr["beginPeriod"].ToString());
                       
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
