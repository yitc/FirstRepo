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
    public class AccPaymentBUS
    {
        private AccPaymentDAO accPaymentDAO;

        public AccPaymentBUS()
        {
            accPaymentDAO = new AccPaymentDAO();
        }

        public bool Save(AccPaymentModel accPayment, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accPaymentDAO.Save(accPayment, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(AccPaymentModel accPayment, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = accPaymentDAO.Update(accPayment, nameForm, idUser);
            }
            catch(Exception ex)
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
                retval = accPaymentDAO.Delete(id, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<IModel> GetAllAccPayment()
        {
            DataTable dataTable = new DataTable();
            dataTable = accPaymentDAO.GetAllAccPayment();
            List<IModel> compList = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AccPaymentModel model = new AccPaymentModel();

                        model.idPayment = Int32.Parse(dr["idPayment"].ToString());

                        if (dr["numberDays"].ToString() != " ")
                        {
                            model.numberDays = Int32.Parse(dr["numberDays"].ToString());
                        }

                        if (dr["isDebitor"].ToString() != " ")
                        {
                            model.isDebitor = bool.Parse(dr["isDebitor"].ToString());
                        }

                        if (dr["isCreditor"].ToString() != " ")
                        {
                            model.isCreditor = bool.Parse(dr["isCreditor"].ToString());
                        }
                        model.description = dr["description"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                    return null;
            }
            else return null;
        }

        public List<AccDebCreModel> GetAccPaymentForDelete(int idPayment)  
        {
            DataTable dataTable = new DataTable();
            dataTable = accPaymentDAO.GetAccPaymentForDelete(idPayment);
            List<AccDebCreModel> compList = new List<AccDebCreModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccDebCreModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDebCreModel();
                        if (dr["idAccDebCre"].ToString() != "")
                            model.idAccDebCre = Int32.Parse(dr["idAccDebCre"].ToString());

                        if (dr["idContPerson"].ToString() != "")
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());


                        //model.namePerson = dr["namePerson"].ToString();
                        ////  debcre.nameClient = dr["nameClient"].ToString();

                        //if (dr["isDebitor"].ToString() != "")
                        //    model.isDebitor = Boolean.Parse(dr["isDebitor"].ToString());
                        //if (dr["isCreditor"].ToString() != "")
                        //    model.isCreditor = Boolean.Parse(dr["isCreditor"].ToString());

                        //if (dr["debAccount"].ToString() != "")
                        //    model.debAccount = dr["debAccount"].ToString();
                        //if (dr["debNameAccount"].ToString() != "")
                        //    model.debNameAccount = dr["debNameAccount"].ToString();

                        //if (dr["creditAccount"].ToString() != "")
                        //    model.creditAccount = dr["creditAccount"].ToString();
                        //if (dr["creNameAccount"].ToString() != "")
                        //    model.creNameAccount = dr["creNameAccount"].ToString();
                        if (dr["payCondition"].ToString() != "")
                            model.payCondition = Int32.Parse(dr["payCondition"].ToString());
                        //if (dr["iban"].ToString() != "")
                        //    model.iban = dr["iban"].ToString();
                        //if (dr["accNumber"].ToString() != "")
                        //    model.accNumber = dr["accNumber"].ToString();


                        compList.Add(model);
                    }
                    return compList;
                }
                else
                    return null;
            }
            else return null;
        }


        public List<AccPaymentModel> GetAllPayments()
        {
            DataTable dataTable = new DataTable();
            dataTable = accPaymentDAO.GetAllAccPayment();
            List<AccPaymentModel> compList = new List<AccPaymentModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AccPaymentModel model = new AccPaymentModel();

                        model.idPayment = Int32.Parse(dr["idPayment"].ToString());

                        if (dr["numberDays"].ToString() != " ")
                        {
                            model.numberDays = Int32.Parse(dr["numberDays"].ToString());
                        }

                        if (dr["isDebitor"].ToString() != " ")
                        {
                            model.isDebitor = bool.Parse(dr["isDebitor"].ToString());
                        }

                        if (dr["isCreditor"].ToString() != " ")
                        {
                            model.isCreditor = bool.Parse(dr["isCreditor"].ToString());
                        }
                        model.description = dr["description"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccPaymentModel GetPaymentByID(int idPayment)
        {
            DataTable dataTable = new DataTable();
            dataTable = accPaymentDAO.GetAccPaymentByID(idPayment);

            AccPaymentModel accPayment = new AccPaymentModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    AccPaymentModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccPaymentModel();

                        model.idPayment = Int32.Parse(dr["idPayment"].ToString());

                        if (dr["numberDays"].ToString() != " ")
                        {
                            model.numberDays = Int32.Parse(dr["numberDays"].ToString());
                        }

                        if (dr["isDebitor"].ToString() != " ")
                        {
                            model.isDebitor = bool.Parse(dr["isDebitor"].ToString());
                        }

                        if (dr["isCreditor"].ToString() != " ")
                        {
                            model.isCreditor = bool.Parse(dr["isCreditor"].ToString());
                        }
                        model.description = dr["description"].ToString();
                    }
                    return model;
                }
                else return null;
            }
            else return null;
        }
    }
    
}