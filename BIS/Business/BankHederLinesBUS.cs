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
    public class BankHederLinesBUS
    {
        private BankHederLinesDAO bankHederLinesDAO;

        public BankHederLinesBUS()
        {
            bankHederLinesDAO = new BankHederLinesDAO();
        }

        public bool Save(BankHederModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = bankHederLinesDAO.Save(model,nameForm,idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(BankHederModel model)
        {
            bool retval = false;
            try
            {
                retval = bankHederLinesDAO.Update(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool UpdateHeder(BankHederModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = bankHederLinesDAO.UpdateEndHeder(model, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool SaveLines(BankLinesModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = bankHederLinesDAO.SaveLines(model, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool UpdateLines(BankLinesModel model)
        {
            bool retval = false;
            try
            {
                retval = bankHederLinesDAO.UpdateLines(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        //public bool Delete(int id)
        //{
        //    bool retval = false;
        //    try
        //    {
        //        retval = accOpenLinesDAO.Delete(id);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return retval;
        //}
        //public bool DeleteByInvoice(string invoice)
        //{
        //    bool retval = false;
        //    try
        //    {
        //        retval = accOpenLinesDAO.DeleteByInvoice(invoice);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return retval;
        //}


        public List<BankLinesModel> GetLinesByHeder(int heder)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = bankHederLinesDAO.GetLinesByHeder(heder);
                List<BankLinesModel> openLines = new List<BankLinesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            BankLinesModel model = new BankLinesModel();


                            model.idBankLine = Int32.Parse(dr["idBankLine"].ToString());
                            model.idBankHeder = Int32.Parse(dr["idBankHeder"].ToString());
                            if (dr["debcreLine"].ToString() != "")
                                model.debcreLine = dr["debcreLine"].ToString();
                            model.idCustomer = dr["idCustomer"].ToString();
                            if (dr["amountLine"].ToString() != "")
                                model.amountLine = Decimal.Parse(dr["amountLine"].ToString());
                            model.transactType = dr["transactType"].ToString();
                            if (dr["valueDate"].ToString() != "")
                                model.valueDate = DateTime.Parse(dr["valueDate"].ToString());
                            model.accountNo = dr["accountNo"].ToString();
                            model.payerLine = dr["payerLine"].ToString();
                            model.refNo = dr["refNo"].ToString();
                            model.desc1Line = dr["desc1Line"].ToString();
                            model.desc2Line = dr["desc2Line"].ToString();
                            model.desc3Line = dr["desc3Line"].ToString();
                            model.desc4Line = dr["desc4Line"].ToString();
                            model.desc5Line = dr["desc5Line"].ToString();
                            model.desc6Line = dr["desc6Line"].ToString();
                            model.desc7Line = dr["desc7Line"].ToString();
                            model.desc8Line = dr["desc8Line"].ToString();
                            model.desc9Line = dr["desc9Line"].ToString();



                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
     
        public List<BankHederModel> CheckHeder(string statement)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = bankHederLinesDAO.CheckHeder(statement);
                List<BankHederModel> openLines = new List<BankHederModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            BankHederModel model = new BankHederModel();


                            model.idBankHeder = Int32.Parse(dr["idBankHeder"].ToString());
                            if (dr["entryDate"].ToString() != "")
                                model.entryDate = DateTime.Parse(dr["entryDate"].ToString());
                            model.statementNo = dr["statementNo"].ToString();
                            model.accountNumber = dr["accountNumber"].ToString();
                            if (dr["dateStatPrevius"].ToString() != "")
                                model.dateStatPrevius = DateTime.Parse(dr["dateStatPrevius"].ToString());
                            if (dr["amountPrevius"].ToString() != "")
                                model.amountPrevius = Decimal.Parse(dr["amountPrevius"].ToString());
                            model.debcrePrevius = dr["debcrePrevius"].ToString();
                            model.debcreEnd = dr["debcreEnd"].ToString();
                            if (dr["dateEnd"].ToString() != "")
                                model.dateEnd = DateTime.Parse(dr["dateEnd"].ToString());
                            if (dr["amountEnd"].ToString() != "")
                                model.amountEnd = Decimal.Parse(dr["amountEnd"].ToString());
                                                

                            openLines.Add(model);
                        }
                        return openLines;
                    }
                    else
                        return openLines;
                }
                else
                    return openLines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}