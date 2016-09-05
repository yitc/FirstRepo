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
    public class InvoiceBUS
    {
        private InvoiceDAO invoiceDAO;

        public InvoiceBUS()
        {
            invoiceDAO = new InvoiceDAO();
        }

        public bool Save(InvoiceModel invoice, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = invoiceDAO.Save(invoice,nameForm,idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(InvoiceModel invoice, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = invoiceDAO.Update(invoice, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public bool UpdateStatus(int status, int invoice, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = invoiceDAO.UpdateStatus(status, invoice, nameForm, idUser);
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
                retval = invoiceDAO.Detele(id, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public InvoicePaidModel GetInvoicePaid(int idInvoice)
        {

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoicePaid(idInvoice);
            InvoicePaidModel model = new InvoicePaidModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());

                        if (dr["paid"].ToString() != "")
                        {
                            model.paid = Decimal.Parse(dr["paid"].ToString());
                        }

                        if (dr["dtUntil"].ToString() != "")
                        {
                            model.dtUntil = DateTime.Parse(dr["dtUntil"].ToString());
                        }
                        break;

                    }
                }
            }
            return model;
        }

        public List<IModel>GetAllInvoices()
        {
            List<IModel> compList = new List<IModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetAllInvoice();

            if(dataTable !=null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
 

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<InvoiceModel> GetAllInvoicesByVoucher(int idVoucher)
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetAllInvoiceByVoucher(idVoucher);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();


                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<InvoiceModel> GetAllInvoice()
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetAllInvoice();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        InvoiceModel model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        public List<InvoiceModel> GetAllInvoiceCustomer(int idDebCre, bool isFrmClient)
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            if (isFrmClient)
            {
                dataTable = invoiceDAO.GetAllInvoiceCustomerC(idDebCre);
            }
            else 
            {
                dataTable = invoiceDAO.GetAllInvoiceCustomerP(idDebCre);
            }
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {

                            InvoiceModel model = new InvoiceModel();

                            model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                            model.invoiceNr = dr["invoiceNr"].ToString();
                            model.invoiceRbr = dr["invoiceRbr"].ToString();
                            if (dr["idVoucher"].ToString() != "")
                            {
                                model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                            }

                            if (dr["idInvoiceStatus"].ToString() != "")
                            {
                                model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                            }

                            model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                            if (dr["idClient"].ToString() != "")
                            {
                                model.idClient = Int32.Parse(dr["idClient"].ToString());
                            }

                            if (dr["idContPerson"].ToString() != "")
                            {
                                model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                            }

                            if (dr["dtInvoice"].ToString() != "")
                            {
                                model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                            }

                            if (dr["dtValuta"].ToString() != "")
                            {
                                model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                            }

                            if (dr["brutoAmount"].ToString() != "")
                            {
                                model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                            }

                            if (dr["netoAmount"].ToString() != "")
                            {
                                model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                            }

                            if (dr["idBtw"].ToString() != "")
                            {
                                model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                            }

                            if (dr["isBooked"].ToString() != "")
                            {
                                model.isBooked = bool.Parse(dr["isBooked"].ToString());
                            }

                            model.noteInvoice = dr["noteInvoice"].ToString();

                            if (dr["dtCreated"].ToString() != "")
                                model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                            if (dr["dtModified"].ToString() != "")
                                model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                            if (dr["userCreated"].ToString() != "")
                                model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                            if (dr["userModified"].ToString() != "")
                                model.userModified = Int32.Parse(dr["userModified"].ToString());
                            if (dr["dtFirstPay"].ToString() != "")
                                model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                            if (dr["dtLastPay"].ToString() != "")
                                model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                            if (dr["percentFrstPay"].ToString() != "")
                            {
                                model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                            }
                            if (dr["reservationCost"].ToString() != "")
                            {
                                model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                            }
                            model.firstreferencePay = dr["firstreferencePay"].ToString();
                            model.secondreferencePay = dr["secondreferencePay"].ToString();
                            if (dr["typeinvoice"].ToString() != "")
                                model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                            if (dr["roomComment"].ToString() != "")
                                model.roomComment = dr["roomComment"].ToString();

                            compList.Add(model);
                        }
                        return compList;
                    }
                    else
                        return null;
                }
                else
                {
                    return null;
                }
        }
        public InvoiceModel GetInvoiceByID(string idInvoice)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoiceByID(idInvoice);

            InvoiceModel invoice = new InvoiceModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();
                        

                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<InvoiceModel> GetBasicInvoices(string idInvoice)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetBasicInvoices(idInvoice);

            List<InvoiceModel> invoice = new List<InvoiceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
                        invoice.Add(model);
                    }
                    
                }
            }
            return invoice;
        }


        public InvoiceModel GetInvoiceByInvoiceAndExtension999(string idInvoice, string ext,bool isCanceled)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoiceByInvoiceAndExtension999(idInvoice, ext,isCanceled);

            InvoiceModel invoice = new InvoiceModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public InvoiceModel GetInvoiceByInvoiceAndExtension(string idInvoice, string ext)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoiceByInvoiceAndExtension(idInvoice, ext);

            InvoiceModel invoice = new InvoiceModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public InvoiceModel GetInvoiceByIntID(int idInvoice)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoiceByIntID(idInvoice);

            InvoiceModel invoice = new InvoiceModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<InvoiceModel> GetInvoiceCustomerAndVoucher(int idVoucher)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoiceCustomerAndVoucher(idVoucher);

            List<InvoiceModel> invoice = new List<InvoiceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["descriptionInvoice"].ToString() != "")
                        {
                            model.descriptionInvoice = dr["descriptionInvoice"].ToString();
                        }
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        invoice.Add(model);
                    }
                    return invoice;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public int GetCountExtension(int idVoucher)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetCountExtension(idVoucher);

           // List<InvoiceModel> invoice = new List<InvoiceModel>();
            int number = 0;
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                  //  InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                      

                        number = Int32.Parse(dr["number"].ToString());
                   
                    }
                    return number;
                }
                else
                    return 0;
            }
            else
                return 0;
        }


        public int GetNumberCancelInsurance(int idVoucher)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetNumberCancelInsurance(idVoucher);

            // List<InvoiceModel> invoice = new List<InvoiceModel>();
            int number = 1;
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        number = Int32.Parse(dr["number"].ToString());

                    }
                }
            }

            if (number == 0)
                number = 1;

            return number;
        }

        public DataTable GetReportInvoiceByIntID(int idInvoice)
        {
            DataTable Invoice = new DataTable();
            Invoice = invoiceDAO.GetReportInvoiceByIntID(idInvoice);

            InvoiceReportModel invoice = new InvoiceReportModel();

            if (Invoice != null)
            {
                if (Invoice.Rows.Count > 0)
                {
                    InvoiceReportModel model = null;

                    foreach (DataRow dr in Invoice.Rows)
                    {
                        model = new InvoiceReportModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        if (dr["street"].ToString() != "")
                            model.street = dr["street"].ToString();
                        if (dr["houseNr"].ToString() != "")
                            model.houseNr = dr["houseNr"].ToString();
                        if (dr["extend"].ToString() != "")
                            model.extend = dr["extend"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["City"].ToString() != "")
                            model.City = dr["City"].ToString();
                        if (dr["country"].ToString() != "")
                            model.country = dr["country"].ToString();
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();

                        if (dr["arrName"].ToString() != "")
                            model.arrName = dr["arrName"].ToString();
                        if (dr["noDays"].ToString() != "")
                            model.noDays = dr["noDays"].ToString();
                        if (dr["boarding"].ToString() != "")
                            model.boarding = dr["boarding"].ToString();
                        if (dr["dateFrom"].ToString() != "")
                            model.dateFrom =dr["dateFrom"].ToString();
                        if (dr["dateTo"].ToString() != "")
                            model.dateTo = dr["dateTo"].ToString();
                         //if (dr["firstAmount"].ToString() != "")
                         //    model.firstAmount = Convert.ToDecimal(dr["firstAmount"].ToString());
                         //if (dr["restAmount"].ToString() != "")
                         //    model.restAmount = Convert.ToDecimal(dr["restAmount"].ToString());
                        if (dr["firstAmount"].ToString() != "")
                            model.firstAmount = dr["firstAmount"].ToString();
                        if (dr["restAmount"].ToString() != "")
                            model.restAmount = dr["restAmount"].ToString();
                         if (dr["firstReference"].ToString() != "")
                             model.firstReference = dr["firstReference"].ToString();
                         if (dr["restReference"].ToString() != "")
                             model.restReference = dr["restReference"].ToString();
                         model.firstreferencePay = dr["firstreferencePay"].ToString();
                         model.secondreferencePay = dr["secondreferencePay"].ToString();

                         if (dr["roomComment"].ToString() != "")
                             model.roomComment = dr["roomComment"].ToString();
                         
                    }
                    return Invoice;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public DataTable GetReportInvoiceFor999ByIntID(int idInvoice)
        {
            DataTable Invoice = new DataTable();
            Invoice = invoiceDAO.GetReportInvoiceFor999ByIntID(idInvoice);

            InvoiceReportModel invoice = new InvoiceReportModel();

            if (Invoice != null)
            {
                if (Invoice.Rows.Count > 0)
                {
                    InvoiceReportModel model = null;

                    foreach (DataRow dr in Invoice.Rows)
                    {
                        model = new InvoiceReportModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        if (dr["street"].ToString() != "")
                            model.street = dr["street"].ToString();
                        if (dr["houseNr"].ToString() != "")
                            model.houseNr = dr["houseNr"].ToString();
                        if (dr["extend"].ToString() != "")
                            model.extend = dr["extend"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["City"].ToString() != "")
                            model.City = dr["City"].ToString();
                        if (dr["country"].ToString() != "")
                            model.country = dr["country"].ToString();
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();

                        if (dr["arrName"].ToString() != "")
                            model.arrName = dr["arrName"].ToString();
                        if (dr["noDays"].ToString() != "")
                            model.noDays = dr["noDays"].ToString();
                        if (dr["boarding"].ToString() != "")
                            model.boarding = dr["boarding"].ToString();
                        if (dr["dateFrom"].ToString() != "")
                            model.dateFrom = dr["dateFrom"].ToString();
                        if (dr["dateTo"].ToString() != "")
                            model.dateTo = dr["dateTo"].ToString();
                        //if (dr["firstAmount"].ToString() != "")
                        //    model.firstAmount = Convert.ToDecimal(dr["firstAmount"].ToString());
                        //if (dr["restAmount"].ToString() != "")
                        //    model.restAmount = Convert.ToDecimal(dr["restAmount"].ToString());
                        if (dr["firstAmount"].ToString() != "")
                            model.firstAmount = dr["firstAmount"].ToString();
                        if (dr["restAmount"].ToString() != "")
                            model.restAmount = dr["restAmount"].ToString();
                        if (dr["firstReference"].ToString() != "")
                            model.firstReference = dr["firstReference"].ToString();
                        if (dr["restReference"].ToString() != "")
                            model.restReference = dr["restReference"].ToString();
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                    }
                    return Invoice;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public DataTable GetReportInvoiceByIntID999(int idInvoice)
        {
            DataTable Invoice = new DataTable();
            Invoice = invoiceDAO.GetReportInvoiceByIntID999(idInvoice);

            InvoiceReportModel invoice = new InvoiceReportModel();

            if (Invoice != null)
            {
                if (Invoice.Rows.Count > 0)
                {
                    InvoiceReportModel model = null;

                    foreach (DataRow dr in Invoice.Rows)
                    {
                        model = new InvoiceReportModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        if (dr["street"].ToString() != "")
                            model.street = dr["street"].ToString();
                        if (dr["houseNr"].ToString() != "")
                            model.houseNr = dr["houseNr"].ToString();
                        if (dr["extend"].ToString() != "")
                            model.extend = dr["extend"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["City"].ToString() != "")
                            model.City = dr["City"].ToString();
                        if (dr["country"].ToString() != "")
                            model.country = dr["country"].ToString();
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();

                        if (dr["arrName"].ToString() != "")
                            model.arrName = dr["arrName"].ToString();
                        if (dr["noDays"].ToString() != "")
                            model.noDays = dr["noDays"].ToString();
                        if (dr["boarding"].ToString() != "")
                            model.boarding = dr["boarding"].ToString();
                        if (dr["dateFrom"].ToString() != "")
                            model.dateFrom = dr["dateFrom"].ToString();
                        if (dr["dateTo"].ToString() != "")
                            model.dateTo = dr["dateTo"].ToString();
                        //if (dr["firstAmount"].ToString() != "")
                        //    model.firstAmount = Convert.ToDecimal(dr["firstAmount"].ToString());
                        //if (dr["restAmount"].ToString() != "")
                        //    model.restAmount = Convert.ToDecimal(dr["restAmount"].ToString());
                        if (dr["firstAmount"].ToString() != "")
                            model.firstAmount = dr["firstAmount"].ToString();
                        if (dr["restAmount"].ToString() != "")
                            model.restAmount = dr["restAmount"].ToString();
                        if (dr["firstReference"].ToString() != "")
                            model.firstReference = dr["firstReference"].ToString();
                        if (dr["restReference"].ToString() != "")
                            model.restReference = dr["restReference"].ToString();
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                    }
                    return Invoice;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<InvoiceModel> GetInvoicesForPrint(int idArrangement, int invStatus)
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoicesForPrint(idArrangement, invStatus);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.select = bool.Parse("true");

                        model.namePerson = dr["namePerson"].ToString();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descInvoiceStatus = dr["descInvoicestatus"].ToString();
                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<InvoiceModel> GetInvoicesForPrintForBooking(int idArrangement, int invStatus)
        {

            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoicesForPrintForBooking(idArrangement, invStatus);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.select = bool.Parse("false");

                        model.namePerson = dr["namePerson"].ToString();

                        if (dr["isInvoicing"].ToString() != "")
                            model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                        model.email = dr["email"].ToString();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descInvoiceStatus = dr["descInvoicestatus"].ToString();
                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Boolean checkIfThereIsInvoiceForCancelingWithBasicNotForCanceling(int idArrangementBook)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.checkIfThereIsInvoiceForCancelingWithBasicNotForCanceling(idArrangementBook);

            // List<InvoiceModel> invoice = new List<InvoiceModel>();
            Boolean res = false;
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr["number"].ToString() != "")
                            if (Convert.ToInt32(dr["number"].ToString()) > 0)
                                res = true;
                        break;
                    }
                }
            }
            return res;
        }

        public List<InvoiceModel> GetInvoicesForPrintForBookingByLabel(int idLabel, int invStatus)
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoicesForPrintForBookingByLabel(idLabel, invStatus);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.select = bool.Parse("false");

                        model.namePerson = dr["namePerson"].ToString();

                        if (dr["isInvoicing"].ToString() != "")
                            model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                        model.email = dr["email"].ToString();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descInvoiceStatus = dr["descInvoicestatus"].ToString();
                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public decimal GetSumInvoicePerson(string invoice, Boolean isSumNotRest)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetSumInvoicePerson(invoice, isSumNotRest);

            // List<InvoiceModel> invoice = new List<InvoiceModel>();
            decimal number = 0;
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {


                        number = Decimal.Parse(dr["number"].ToString());

                    }
                    return number;
                }
                else
                    return 0;
            }
            else
                return 0;
        }

        public decimal GetSumCancelInsurance(string invoice)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetSumCancelInsurance(invoice);

            // List<InvoiceModel> invoice = new List<InvoiceModel>();
            decimal number = 0;
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //  InvoiceModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {


                        number = Decimal.Parse(dr["number"].ToString());

                    }
                    return number;
                }
                else
                    return 0;
            }
            else
                return 0;
        }



        public InvoiceModel GetInvoiceByVoucher(int idVoucher)
        {
            DataTable Invoice = new DataTable();
            InvoiceModel model = new InvoiceModel();

            Invoice = invoiceDAO.GetInvoiceByVoucher(idVoucher);

            

            if (Invoice != null)
            {
                if (Invoice.Rows.Count > 0)
                {
                   // InvoiceModel model = null;

                    foreach (DataRow dr in Invoice.Rows)
                   {
                        model = new InvoiceModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
                 
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<InvoiceModel> GetInvoicesForAccounting(int idArrangement, int invStatus)
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoicesForAccounting(idArrangement, invStatus);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.select = bool.Parse("false");

                        model.namePerson = dr["namePerson"].ToString();

                        if (dr["isInvoicing"].ToString() != "")
                            model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                        model.email = dr["email"].ToString();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descInvoiceStatus = dr["descInvoicestatus"].ToString();
                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public List<InvoiceModel> GetInvoicesForAccountingByLabel(int idLabel, int invStatus)
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetInvoicesForAccountingByLabel(idLabel, invStatus);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.select = bool.Parse("false");

                        model.namePerson = dr["namePerson"].ToString();

                        if (dr["isInvoicing"].ToString() != "")
                            model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                        model.email = dr["email"].ToString();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descInvoiceStatus = dr["descInvoicestatus"].ToString();
                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public List<InvoiceModel> GetAllInvoicesForAccounting(int invStatus)
        {
            List<InvoiceModel> compList = new List<InvoiceModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceDAO.GetAllInvoicesForAccounting(invStatus);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceModel model = new InvoiceModel();

                        model.select = bool.Parse("false");

                        model.namePerson = dr["namePerson"].ToString();

                        if (dr["isInvoicing"].ToString() != "")
                            model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());

                        model.email = dr["email"].ToString();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();

                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }

                        model.descInvoiceStatus = dr["descInvoicestatus"].ToString();
                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();
                        if (dr["typeinvoice"].ToString() != "")
                            model.typeinvoice = Int32.Parse(dr["typeinvoice"].ToString());

                         if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public DataTable GetReportInvoiceReportByIntID(int idInvoice)
        {
            DataTable Invoice = new DataTable();
            Invoice = invoiceDAO.GetReportInvoiceReportByIntID(idInvoice);

            InvoiceReportModel invoice = new InvoiceReportModel();

            if (Invoice != null)
            {
                if (Invoice.Rows.Count > 0)
                {
                    InvoiceReportModel model = null;

                    foreach (DataRow dr in Invoice.Rows)
                    {
                        model = new InvoiceReportModel();

                        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        model.invoiceNr = dr["invoiceNr"].ToString();
                        model.invoiceRbr = dr["invoiceRbr"].ToString();
                        if (dr["idVoucher"].ToString() != "")
                        {
                            model.idVoucher = Int32.Parse(dr["idVoucher"].ToString());
                        }

                        if (dr["idInvoiceStatus"].ToString() != "")
                        {
                            model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        }
                        if (dr["descInvoiceStatus"].ToString() != "")
                            model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();


                        model.descriptionInvoice = dr["descriptionInvoice"].ToString();

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idClient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["dtInvoice"].ToString() != "")
                        {
                            model.dtInvoice = Convert.ToDateTime(dr["dtInvoice"].ToString());
                        }

                        if (dr["dtValuta"].ToString() != "")
                        {
                            model.dtValuta = Convert.ToDateTime(dr["dtValuta"].ToString());
                        }

                        if (dr["brutoAmount"].ToString() != "")
                        {
                            model.brutoAmount = Convert.ToDecimal(dr["brutoAmount"].ToString());
                        }

                        if (dr["netoAmount"].ToString() != "")
                        {
                            model.netoAmount = Convert.ToDecimal(dr["netoAmount"].ToString());
                        }

                        if (dr["idBtw"].ToString() != "")
                        {
                            model.idBtw = Int32.Parse(dr["idBtw"].ToString());
                        }

                        if (dr["isBooked"].ToString() != "")
                        {
                            model.isBooked = bool.Parse(dr["isBooked"].ToString());
                        }

                        model.noteInvoice = dr["noteInvoice"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["dtFirstPay"].ToString() != "")
                            model.dtFirstPay = DateTime.Parse(dr["dtFirstPay"].ToString());
                        if (dr["dtLastPay"].ToString() != "")
                            model.dtLastPay = DateTime.Parse(dr["dtLastPay"].ToString());
                        if (dr["percentFrstPay"].ToString() != "")
                        {
                            model.percentFrstPay = Convert.ToDecimal(dr["percentFrstPay"].ToString());
                        }
                        if (dr["reservationCost"].ToString() != "")
                        {
                            model.reservationCost = Convert.ToDecimal(dr["reservationCost"].ToString());
                        }
                        if (dr["street"].ToString() != "")
                            model.street = dr["street"].ToString();
                        if (dr["houseNr"].ToString() != "")
                            model.houseNr = dr["houseNr"].ToString();
                        if (dr["extend"].ToString() != "")
                            model.extend = dr["extend"].ToString();
                        if (dr["zip"].ToString() != "")
                            model.zip = dr["zip"].ToString();
                        if (dr["City"].ToString() != "")
                            model.City = dr["City"].ToString();
                        if (dr["country"].ToString() != "")
                            model.country = dr["country"].ToString();
                        if (dr["namePerson"].ToString() != "")
                            model.namePerson = dr["namePerson"].ToString();

                        if (dr["arrName"].ToString() != "")
                            model.arrName = dr["arrName"].ToString();
                        if (dr["noDays"].ToString() != "")
                            model.noDays = dr["noDays"].ToString();
                        if (dr["boarding"].ToString() != "")
                            model.boarding = dr["boarding"].ToString();
                        if (dr["dateFrom"].ToString() != "")
                            model.dateFrom = dr["dateFrom"].ToString();
                        if (dr["dateTo"].ToString() != "")
                            model.dateTo = dr["dateTo"].ToString();
                        //if (dr["firstAmount"].ToString() != "")
                        //    model.firstAmount = Convert.ToDecimal(dr["firstAmount"].ToString());
                        //if (dr["restAmount"].ToString() != "")
                        //    model.restAmount = Convert.ToDecimal(dr["restAmount"].ToString());
                        if (dr["firstAmount"].ToString() != "")
                            model.firstAmount = dr["firstAmount"].ToString();
                        if (dr["restAmount"].ToString() != "")
                            model.restAmount = dr["restAmount"].ToString();
                        if (dr["firstReference"].ToString() != "")
                            model.firstReference = dr["firstReference"].ToString();
                        if (dr["restReference"].ToString() != "")
                            model.restReference = dr["restReference"].ToString();
                        model.firstreferencePay = dr["firstreferencePay"].ToString();
                        model.secondreferencePay = dr["secondreferencePay"].ToString();

                        if (dr["roomComment"].ToString() != "")
                            model.roomComment = dr["roomComment"].ToString();
                    }
                    return Invoice;
                }
                else
                    return null;
            }
            else
                return null;
        }


    }
    
}