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
    public class AccCreditLinePayBUS
    {
        private AccCreditLinePayDAO creditlinepayDAO;

        public AccCreditLinePayBUS()
        {
            creditlinepayDAO = new AccCreditLinePayDAO();
        }

        public bool Save(AccCreditLinePayModel linesmodel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = creditlinepayDAO.Save(linesmodel, nameForm, idUser);

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

                retval = creditlinepayDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
       

        //===================
        public List<AccCreditLinePayModel> GetAllLinesByCreditor(string creditor, string invoice)
        {
            DataTable dataTable = new DataTable();
            dataTable = creditlinepayDAO.GetAllLinesByCreditor(creditor, invoice);
            List<AccCreditLinePayModel> linesmodel = new List<AccCreditLinePayModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccCreditLinePayModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccCreditLinePayModel();

                        model.idCreditLinePay = Int32.Parse(dr["idCreditLinePay"].ToString());
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();

                        if (dr["invoiceNr"].ToString() != "")
                            model.invoiceNr = dr["invoiceNr"].ToString();
                        if (dr["term"].ToString() != "")
                            model.term = Int32.Parse(dr["term"].ToString());
                        if (dr["percentpay"].ToString() != "")
                            model.percentpay = Decimal.Parse(dr["percentpay"].ToString());
                        if (dr["amount"].ToString() != "")
                            model.amount = Decimal.Parse(dr["amount"].ToString());
                        if (dr["dtDate"].ToString() != "")
                            model.dtDate = DateTime.Parse(dr["dtDate"].ToString());


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