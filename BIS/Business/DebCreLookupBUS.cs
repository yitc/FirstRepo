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
    public class DebCreLookupBUS
    {
        private AccDebCreDAO accdebcreDAO;

        public DebCreLookupBUS()
        {
            accdebcreDAO = new AccDebCreDAO();
        }




        public List<IModel> GetCreditors()
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetCreditors();
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    DebCreLookupModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model = new DebCreLookupModel();
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();


                        if (dr["name"].ToString() != "")
                            model.name = dr["name"].ToString();

                        if (dr["address"].ToString() != "")
                            model.address = dr["address"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["city"].ToString() != "")
                            model.city = dr["city"].ToString();
                     
                         debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<IModel> GetCreditorsCredPay()
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetCreditorsCredPay();
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    DebCreLookupModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model = new DebCreLookupModel();
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();


                        if (dr["name"].ToString() != "")
                            model.name = dr["name"].ToString();

                        if (dr["address"].ToString() != "")
                            model.address = dr["address"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["city"].ToString() != "")
                            model.city = dr["city"].ToString();

                        debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<IModel> GetDebitors()
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetDebitors();
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    DebCreLookupModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model = new DebCreLookupModel();

                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();
                        if (dr["name"].ToString() != "")
                            model.name = dr["name"].ToString();
                        if (dr["address"].ToString() != "")
                            model.address = dr["address"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["city"].ToString() != "")
                            model.city = dr["city"].ToString();                        

                        debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetDebitorsPerson()
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetDebitorsPerson();
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    DebCreLookupModelAdvanced model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model = new DebCreLookupModelAdvanced();

                        if (dr["idContPerson"].ToString() != "")
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());

                        model.street = dr["street"].ToString();
                        model.housenr = dr["housenr"].ToString();
                        model.extension = dr["extension"].ToString();
                        model.country = dr["country"].ToString();

                        model.accNumber = dr["accNumber"].ToString();
                        model.name = dr["name"].ToString();
                        model.zip = dr["zip"].ToString();
                        model.city = dr["city"].ToString();

                        debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetDebitorsClient()
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetDebitorsClient();
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    DebCreLookupModelAdvanced model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model = new DebCreLookupModelAdvanced();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        model.street = dr["street"].ToString();
                        model.housenr = dr["housenr"].ToString();
                        model.extension = dr["extension"].ToString();
                        model.country = dr["country"].ToString();

                        model.accNumber = dr["accNumber"].ToString();
                        model.name = dr["name"].ToString();    
                        model.zip = dr["zip"].ToString();
                        model.city = dr["city"].ToString();

                        debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetCreditorName(string accname)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetCreditorName(accname);
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    DebCreLookupModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        model = new DebCreLookupModel();
                        if (dr["accNumber"].ToString() != "")
                            model.accNumber = dr["accNumber"].ToString();

                        if (dr["name"].ToString() != "")
                            model.name = dr["name"].ToString();

                        if (dr["address"].ToString() != "")
                            model.address = dr["address"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["city"].ToString() != "")
                            model.city = dr["city"].ToString();

                        debcre.Add(model);
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public DataTable GetIDByAccNumber(string accname)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetIDByAccNumber(accname);

            return dataTable;
        }

        public List<IModel> GetAllRange(string from, string to, bool deb, bool isBalans, bool sum, DateTime dtFrom, DateTime dtTo, string nameUser)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetAllRange(from, to, deb, isBalans, sum, dtFrom, dtTo, nameUser);
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    if (isBalans == true)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            DebCreCreditCardBalansModel model = new DebCreCreditCardBalansModel();
                            if (dr["accNumber"].ToString() != "")
                                model.accNumber = dr["accNumber"].ToString();
                            if (dr["name"].ToString() != "")
                                model.name = dr["name"].ToString();
                            if (dr["zip"].ToString() != "")
                                model.zip = dr["zip"].ToString();
                            if (dr["city"].ToString() != "")
                                model.city = dr["city"].ToString();
                            if (dr["address"].ToString() != "")
                                model.address = dr["address"].ToString();
                            if (dr["DebitBalans"].ToString() != "")
                                model.DebitBalans = Decimal.Parse(dr["DebitBalans"].ToString());
                            if (dr["CreditBalans"].ToString() != "")
                                model.CreditBalans = Decimal.Parse(dr["CreditBalans"].ToString());
                            if (dr["Debit"].ToString() != "")
                                model.Debit = Decimal.Parse(dr["Debit"].ToString());
                            if (dr["Credit"].ToString() != "")
                                model.Credit = Decimal.Parse(dr["Credit"].ToString());
                            if (dr["Saldo"].ToString() != "")
                                model.Saldo = Decimal.Parse(dr["Saldo"].ToString());

                            debcre.Add(model);
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            DebCreCreditCardModel model = new DebCreCreditCardModel();
                            if (dr["accNumber"].ToString() != "")
                                model.accNumber = dr["accNumber"].ToString();
                            if (dr["name"].ToString() != "")
                                model.name = dr["name"].ToString();
                            if (dr["zip"].ToString() != "")
                                model.zip = dr["zip"].ToString();
                            if (dr["city"].ToString() != "")
                                model.city = dr["city"].ToString();
                            if (dr["address"].ToString() != "")
                                model.address = dr["address"].ToString();

                            if (dr["Debit"].ToString() != "")
                                model.Debit = Decimal.Parse(dr["Debit"].ToString());
                            if (dr["Credit"].ToString() != "")
                                model.Credit = Decimal.Parse(dr["Credit"].ToString());
                            if (dr["Saldo"].ToString() != "")
                                model.Saldo = Decimal.Parse(dr["Saldo"].ToString());

                            debcre.Add(model);
                        }
                    }
                    return debcre;
                }
                else
                    return null;
            }
            else
                return null;
        }


        /// novo 27 04

        public List<IModel> GetDetailRange(string from, string to, bool deb, bool isBalans, bool sum, DateTime dtFrom, DateTime dtTo, string nameUser)
        {
            DataTable dataTable = new DataTable();
            dataTable = accdebcreDAO.GetDetailRange(from, to, deb, isBalans, sum, dtFrom, dtTo, nameUser);
            List<IModel> debcre = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    if (isBalans == true)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            DebCreCreditCardDetailBalansModel model = new DebCreCreditCardDetailBalansModel();
                            if (dr["accNumber"].ToString() != "")
                                model.accNumber = dr["accNumber"].ToString();
                            if (dr["name"].ToString() != "")
                                model.name = dr["name"].ToString();
                            if (dr["invoiceNr"].ToString() != "")
                                model.invoiceNr = dr["invoiceNr"].ToString();
                            if (dr["incopNr"].ToString() != "")
                                model.incopNr = dr["incopNr"].ToString();


                            if (dr["dtLine"].ToString() != "")
                                model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                            if (dr["periodLine"].ToString() != "")
                                model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                            if (dr["descLine"].ToString() != "")
                                model.descLine = dr["descLine"].ToString();
                            if (dr["DebitBalans"].ToString() != "")
                                model.DebitBalans = Decimal.Parse(dr["DebitBalans"].ToString());
                            if (dr["CreditBalans"].ToString() != "")
                                model.CreditBalans = Decimal.Parse(dr["CreditBalans"].ToString());
                            if (dr["Debit"].ToString() != "")
                                model.Debit = Decimal.Parse(dr["Debit"].ToString());
                            if (dr["Credit"].ToString() != "")
                                model.Credit = Decimal.Parse(dr["Credit"].ToString());
                            if (dr["Saldo"].ToString() != "")
                                model.Saldo = Decimal.Parse(dr["Saldo"].ToString());

                            if (dr["numberLedAccount"].ToString() != "")
                                model.numberLedAccount = dr["numberLedAccount"].ToString();


                            debcre.Add(model);
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            DebCreCreditCardDetailModel model = new DebCreCreditCardDetailModel();
                            if (dr["accNumber"].ToString() != "")
                                model.accNumber = dr["accNumber"].ToString();
                            if (dr["name"].ToString() != "")
                                model.name = dr["name"].ToString();

                            if (dr["invoiceNr"].ToString() != "")
                                model.invoiceNr = dr["invoiceNr"].ToString();
                            if (dr["incopNr"].ToString() != "")
                                model.incopNr = dr["incopNr"].ToString();


                            if (dr["dtLine"].ToString() != "")
                                model.dtLine = DateTime.Parse(dr["dtLine"].ToString());
                            if (dr["periodLine"].ToString() != "")
                                model.periodLine = Int32.Parse(dr["periodLine"].ToString());
                            if (dr["descLine"].ToString() != "")
                                model.descLine = dr["descLine"].ToString();
                            if (dr["Debit"].ToString() != "")
                                model.Debit = Decimal.Parse(dr["Debit"].ToString());
                            if (dr["Credit"].ToString() != "")
                                model.Credit = Decimal.Parse(dr["Credit"].ToString());
                            if (dr["Saldo"].ToString() != "")
                                model.Saldo = Decimal.Parse(dr["Saldo"].ToString());

                            if (dr["numberLedAccount"].ToString() != "")
                                model.numberLedAccount = dr["numberLedAccount"].ToString();

                            if (dr["idAccDaily"].ToString() != "")
                                model.idAccDaily = Int32.Parse(dr["idAccDaily"].ToString());

                            debcre.Add(model);
                        }
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