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
    public class AccDebCreBUS
    {
        private AccDebCreDAO accdebcreDAO;

        public AccDebCreBUS()
        {
            accdebcreDAO = new AccDebCreDAO();
        }



        public bool Save(AccDebCreModel debcre, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = accdebcreDAO.Save(debcre, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Update(AccDebCreModel debcre, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = accdebcreDAO.Update(debcre, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool UpdateDebitorToTrue(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = accdebcreDAO.UpdateDebitorToTrue(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
       
        public AccDebCreModel GetPersonDebCre(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetPersonDebCre(idContPers);
            AccDebCreModel debcre = new AccDebCreModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                 //   AccDebCreModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                      //  model = new AccDebCreModel();
                        if (dr["idAccDebCre"].ToString() != "")
                            debcre.idAccDebCre = Int32.Parse(dr["idAccDebCre"].ToString());

                        if (dr["idContPerson"].ToString() != "")
                            debcre.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            debcre.idClient = Int32.Parse(dr["idClient"].ToString());


                        debcre.namePerson = dr["namePerson"].ToString();
                        //  debcre.nameClient = dr["nameClient"].ToString();

                        if (dr["isDebitor"].ToString() != "")
                            debcre.isDebitor = Boolean.Parse(dr["isDebitor"].ToString());
                        if (dr["isCreditor"].ToString() != "")
                            debcre.isCreditor = Boolean.Parse(dr["isCreditor"].ToString());

                        if (dr["debAccount"].ToString() != "")
                            debcre.debAccount = dr["debAccount"].ToString();
                        if (dr["debNameAccount"].ToString() != "")
                            debcre.debNameAccount = dr["debNameAccount"].ToString();

                        if (dr["creditAccount"].ToString() != "")
                            debcre.creditAccount = dr["creditAccount"].ToString();
                        if (dr["creNameAccount"].ToString() != "")
                            debcre.creNameAccount = dr["creNameAccount"].ToString();
                        if (dr["payCondition"].ToString() != "")
                            debcre.payCondition = Int32.Parse(dr["payCondition"].ToString());
                        if (dr["iban"].ToString() != "")
                            debcre.iban = dr["iban"].ToString();
                        if (dr["accNumber"].ToString() != "")
                            debcre.accNumber = dr["accNumber"].ToString();

                       // debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public AccDebCreModel GetClientDebCre(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetClientDebCre(idClient);
            AccDebCreModel debcre = new AccDebCreModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    //AccDebCreModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                      //  model = new AccDebCreModel();
                        if (dr["idAccDebCre"].ToString() != "")
                            debcre.idAccDebCre = Int32.Parse(dr["idAccDebCre"].ToString());

                        if (dr["idContPerson"].ToString() != "")
                            debcre.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            debcre.idClient = Int32.Parse(dr["idClient"].ToString());


                  //      debcre.namePerson = dr["namePerson"].ToString();
                        debcre.nameClient = dr["nameClient"].ToString();

                        if (dr["isDebitor"].ToString() != "")
                            debcre.isDebitor = Boolean.Parse(dr["isDebitor"].ToString());
                        if (dr["isCreditor"].ToString() != "")
                            debcre.isCreditor = Boolean.Parse(dr["isCreditor"].ToString());

                        if (dr["debAccount"].ToString() != "")
                            debcre.debAccount = dr["debAccount"].ToString();
                        if (dr["debNameAccount"].ToString() != "")
                            debcre.debNameAccount = dr["debNameAccount"].ToString();

                        if (dr["creditAccount"].ToString() != "")
                            debcre.creditAccount = dr["creditAccount"].ToString();
                        if (dr["creNameAccount"].ToString() != "")
                            debcre.creNameAccount = dr["creNameAccount"].ToString();
                        if (dr["payCondition"].ToString() != "")
                            debcre.payCondition = Int32.Parse(dr["payCondition"].ToString());
                        if (dr["iban"].ToString() != "")
                            debcre.iban = dr["iban"].ToString();
                        if (dr["accNumber"].ToString() != "")
                            debcre.accNumber = dr["accNumber"].ToString();

                       // debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;

        }
        public AccDebCreModel GetCustomerByAccCode(string accNumber)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetCustomerByAccCode(accNumber);
            AccDebCreModel debcre = new AccDebCreModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    //AccDebCreModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        //  model = new AccDebCreModel();
                        if (dr["idAccDebCre"].ToString() != "")
                            debcre.idAccDebCre = Int32.Parse(dr["idAccDebCre"].ToString());

                        if (dr["idContPerson"].ToString() != "")
                            debcre.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            debcre.idClient = Int32.Parse(dr["idClient"].ToString());


                        //      debcre.namePerson = dr["namePerson"].ToString();
                       // debcre.nameClient = dr["nameClient"].ToString();

                        if (dr["isDebitor"].ToString() != "")
                            debcre.isDebitor = Boolean.Parse(dr["isDebitor"].ToString());
                        if (dr["isCreditor"].ToString() != "")
                            debcre.isCreditor = Boolean.Parse(dr["isCreditor"].ToString());

                        if (dr["debAccount"].ToString() != "")
                            debcre.debAccount = dr["debAccount"].ToString();
                        if (dr["debNameAccount"].ToString() != "")
                            debcre.debNameAccount = dr["debNameAccount"].ToString();

                        if (dr["creditAccount"].ToString() != "")
                            debcre.creditAccount = dr["creditAccount"].ToString();
                        if (dr["creNameAccount"].ToString() != "")
                            debcre.creNameAccount = dr["creNameAccount"].ToString();
                        if (dr["payCondition"].ToString() != "")
                            debcre.payCondition = Int32.Parse(dr["payCondition"].ToString());
                        if (dr["iban"].ToString() != "")
                            debcre.iban = dr["iban"].ToString();
                        if (dr["accNumber"].ToString() != "")
                            debcre.accNumber = dr["accNumber"].ToString();
                        if (dr["nameClient"].ToString() != "")
                            debcre.nameClient = dr["nameClient"].ToString();
                        if (dr["namePerson"].ToString() != "")
                            debcre.namePerson = dr["namePerson"].ToString();
                        // debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;

        }
        public AccDebCreModel GetCreditorName(string accNumber)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetCreditorName(accNumber);
            AccDebCreModel debcre = new AccDebCreModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    //AccDebCreModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        //  model = new AccDebCreModel();
                        if (dr["idAccDebCre"].ToString() != "")
                            debcre.idAccDebCre = Int32.Parse(dr["idAccDebCre"].ToString());

                        if (dr["idContPerson"].ToString() != "")
                            debcre.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        if (dr["idClient"].ToString() != "")
                            debcre.idClient = Int32.Parse(dr["idClient"].ToString());


                        //      debcre.namePerson = dr["namePerson"].ToString();
                        // debcre.nameClient = dr["nameClient"].ToString();

                        if (dr["isDebitor"].ToString() != "")
                            debcre.isDebitor = Boolean.Parse(dr["isDebitor"].ToString());
                        if (dr["isCreditor"].ToString() != "")
                            debcre.isCreditor = Boolean.Parse(dr["isCreditor"].ToString());

                        if (dr["debAccount"].ToString() != "")
                            debcre.debAccount = dr["debAccount"].ToString();
                        //if (dr["debNameAccount"].ToString() != "")
                        //    debcre.debNameAccount = dr["debNameAccount"].ToString();

                        if (dr["creditAccount"].ToString() != "")
                            debcre.creditAccount = dr["creditAccount"].ToString();
                        //if (dr["creNameAccount"].ToString() != "")
                        //    debcre.creNameAccount = dr["creNameAccount"].ToString();
                        if (dr["payCondition"].ToString() != "")
                            debcre.payCondition = Int32.Parse(dr["payCondition"].ToString());
                        if (dr["iban"].ToString() != "")
                            debcre.iban = dr["iban"].ToString();
                        if (dr["accNumber"].ToString() != "")
                            debcre.accNumber = dr["accNumber"].ToString();
                        //if (dr["nameClient"].ToString() != "")
                        //    debcre.nameClient = dr["nameClient"].ToString();
                        //if (dr["namePerson"].ToString() != "")
                        //    debcre.namePerson = dr["namePerson"].ToString();
                        // debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;

        }

     
      
    }
}

// Do ovde