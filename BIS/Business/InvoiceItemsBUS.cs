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
    public class InvoiceItemsBUS
    {
        private InvoiceItemsDAO invoiceItemsDAO;

        public InvoiceItemsBUS()
        {
            invoiceItemsDAO = new InvoiceItemsDAO();
        }

        public bool Save(InvoiceItemsModel invoiceItems, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = invoiceItemsDAO.Save(invoiceItems, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool SaveItemsTransaction(List<InvoiceItemsModel> invoiceItems, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = invoiceItemsDAO.SaveItemsTransaction(invoiceItems, nameForm,idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Delete (int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = invoiceItemsDAO.Delete(id,nameForm,idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<InvoiceItemsModel> GetInvoiceItemsByInvoice(string invoice, string lang)
        {
            List<InvoiceItemsModel> compList = new List<InvoiceItemsModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceItemsDAO.GetInvoiceItemsByInvoice(invoice, lang);

            if(dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceItemsModel model = new InvoiceItemsModel();

                        model.idInvItem = Int32.Parse(dr["idInvItem"].ToString());

                        if (dr["idInvoice"].ToString() != "")
                        {
                            model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        }

                        if (dr["idArtical"].ToString() != "")
                        {
                            model.idArtical = dr["idArtical"].ToString();
                        }
                        if (dr["nameArtical"].ToString() != "")
                        {
                            model.nameArtical = dr["nameArtical"].ToString();
                        }

                        if (dr["price"].ToString() != "")
                        {
                            model.price = Decimal.Parse(dr["price"].ToString());
                        }

                        if (dr["quantity"].ToString() != "")
                        {
                            model.quantity = Int32.Parse(dr["quantity"].ToString());
                        }
                        if (dr["itemSum"].ToString() != "")
                        {
                            model.itemSum = Decimal.Parse(dr["itemSum"].ToString());
                        }


                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["isSecondGrid"].ToString() != "")
                            model.isSecondGrid = Boolean.Parse(dr["isSecondGrid"].ToString());
                        if (dr["isCancelationIns"].ToString() != "")
                            model.isCancelationIns = Boolean.Parse(dr["isCancelationIns"].ToString());

                        if (dr["isMedical"].ToString() != "")
                            model.isMedical = Boolean.Parse(dr["isMedical"].ToString());

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

        public List<InvoiceItemsModel> GetAllInvoiceItems(string lang)
        {
            List<InvoiceItemsModel> compList = new List<InvoiceItemsModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceItemsDAO.GetAllInvoiceItems(lang);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        InvoiceItemsModel model = new InvoiceItemsModel();

                        model.idInvItem = Int32.Parse(dr["idInvItem"].ToString());

                        if (dr["idInvoice"].ToString() != "")
                        {
                            model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        }

                        if (dr["idArtical"].ToString() != "")
                        {
                            model.idArtical = dr["idArtical"].ToString();
                        }
                        if (dr["nameArtical"].ToString() != "")
                        {
                            model.nameArtical = dr["nameArtical"].ToString();
                        }

                        if (dr["price"].ToString() != "")
                        {
                            model.price = Decimal.Parse(dr["price"].ToString());
                        }

                        if (dr["quantity"].ToString() != "")
                        {
                            model.quantity = Int32.Parse(dr["quantity"].ToString());
                        }
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());
                        if (dr["isSecondGrid"].ToString() != "")
                            model.isSecondGrid = Boolean.Parse(dr["isSecondGrid"].ToString());
                        if (dr["isCancelationIns"].ToString() != "")
                            model.isCancelationIns = Boolean.Parse(dr["isCancelationIns"].ToString());

                        if (dr["isMedical"].ToString() != "")
                            model.isMedical = Boolean.Parse(dr["isMedical"].ToString());

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

        public List<InvoiceItemsModel> GetInvoiceItemsByID(int idInvItem, string lang)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceItemsDAO.GetInvoiceItemsByID(idInvItem, lang);

            List<InvoiceItemsModel> invoiceItem = new List<InvoiceItemsModel>();

            if (dataTable != null)
            {
                InvoiceItemsModel model = new InvoiceItemsModel();
                if (dataTable.Rows.Count > 0)
                {
                    

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceItemsModel();

                        model.idInvItem = Int32.Parse(dr["idInvItem"].ToString());

                        if (dr["idInvoice"].ToString() != "")
                        {
                            model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        }

                        if (dr["idArtical"].ToString() != "")
                        {
                            model.idArtical = dr["idArtical"].ToString();
                        }
                        if (dr["nameArtical"].ToString() != "")
                        {
                            model.nameArtical = dr["nameArtical"].ToString();
                        }
                        if (dr["price"].ToString() != "")
                        {
                            model.price = Decimal.Parse(dr["price"].ToString());
                        }

                        if (dr["quantity"].ToString() != "")
                        {
                            model.quantity = Int32.Parse(dr["quantity"].ToString());
                        }
                        if (dr["itemSum"].ToString() != "")
                        {
                            model.itemSum = Decimal.Parse(dr["itemSum"].ToString());
                        }

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["isSecondGrid"].ToString() != "")
                            model.isSecondGrid = Boolean.Parse(dr["isSecondGrid"].ToString());
                        if (dr["isCancelationIns"].ToString() != "")
                            model.isCancelationIns = Boolean.Parse(dr["isCancelationIns"].ToString());

                        if (dr["isMedical"].ToString() != "")
                            model.isMedical = Boolean.Parse(dr["isMedical"].ToString());
                        invoiceItem.Add(model);

                    }
                    return invoiceItem;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<InvoiceItemsModel> GetInvoiceItemsByInvoiceNrWithPrice(int invoiceNr, string lang)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceItemsDAO.GetInvoiceItemsByInvoiceNrWithPrice(invoiceNr, lang);

            List<InvoiceItemsModel> invoiceItem = new List<InvoiceItemsModel>();

            if (dataTable != null)
            {
                InvoiceItemsModel model = new InvoiceItemsModel();
                if (dataTable.Rows.Count > 0)
                {


                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceItemsModel();

                        model.idInvItem = Int32.Parse(dr["idInvItem"].ToString());

                        if (dr["idInvoice"].ToString() != "")
                        {
                            model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                        }

                        if (dr["idArtical"].ToString() != "")
                        {
                            model.idArtical = dr["idArtical"].ToString();
                        }
                        if (dr["nameArtical"].ToString() != "")
                        {
                            model.nameArtical = dr["nameArtical"].ToString();
                        }
                        if (dr["price"].ToString() != "")
                        {
                            model.price = Decimal.Parse(dr["price"].ToString());
                        }

                        if (dr["quantity"].ToString() != "")
                        {
                            model.quantity = Int32.Parse(dr["quantity"].ToString());
                        }
                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["isSecondGrid"].ToString() != "")
                            model.isSecondGrid = Boolean.Parse(dr["isSecondGrid"].ToString());
                        if (dr["isCancelationIns"].ToString() != "")
                            model.isCancelationIns = Boolean.Parse(dr["isCancelationIns"].ToString());

                        if (dr["isMedical"].ToString() != "")
                            model.isMedical = Boolean.Parse(dr["isMedical"].ToString());

                        invoiceItem.Add(model);

                    }
                    return invoiceItem;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public DataTable GetReportInvoiceItemsByID(int idInvItem, string lang)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceItemsDAO.GetInvoiceItemsByIDNoFirst(idInvItem, lang);

            DataTable invoiceItem = new DataTable();
            invoiceItem = dataTable.Copy();
            
         //   List<InvoiceItemsModel> iim = new List<InvoiceItemsModel>();

            if (dataTable != null)
            {
                InvoiceItemsModel model = new InvoiceItemsModel();
                if (dataTable.Rows.Count > 0)
                {


                    //foreach (DataRow dr in dataTable.Rows)
                    //{
                    //    model = new InvoiceItemsModel();

                    ////    model.idInvItem = Int32.Parse(dr["idInvItem"].ToString());

                    //    if (dr["idInvoice"].ToString() != "")
                    //    {
                    //        model.idInvoice = Int32.Parse(dr["idInvoice"].ToString());
                    //    }

                    //    if (dr["idArtical"].ToString() != "")
                    //    {
                    //        model.idArtical = dr["idArtical"].ToString();
                    //    }
                    //    model.nameArtical = dr["nameArtical"].ToString();
                    //    if (dr["price"].ToString() != "")
                    //    {
                    //        model.price = Decimal.Parse(dr["price"].ToString());
                    //    }

                    //    if (dr["quantity"].ToString() != "")
                    //    {
                    //        model.quantity = Int32.Parse(dr["quantity"].ToString());
                    //    }
                    //    if (dr["dtCreated"].ToString() != "")
                    //        model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());
                    //    if (dr["dtModified"].ToString() != "")
                    //        model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                    //    if (dr["userCreated"].ToString() != "")
                    //        model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                    //    if (dr["userModified"].ToString() != "")
                    //        model.userModified = Int32.Parse(dr["userModified"].ToString());

                    //    if (dr["isSecondGrid"].ToString() != "")
                    //        model.isSecondGrid = Boolean.Parse(dr["isSecondGrid"].ToString());
                    //    if (dr["isCancelationIns"].ToString() != "")
                    //        model.isCancelationIns = Boolean.Parse(dr["isCancelationIns"].ToString());
                     
                    //    invoiceItem.Rows.Add(model);
                    //    //iim.Add(model);

                    //}
                    return invoiceItem;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public DataTable GetReportInvoiceItemsByIDAll(int idInvItem, string lang)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceItemsDAO.GetInvoiceItemsByIDWithFirst(idInvItem,lang);

            DataTable invoiceItem = new DataTable();
            invoiceItem = dataTable.Copy();

            //   List<InvoiceItemsModel> iim = new List<InvoiceItemsModel>();

            if (dataTable != null)
            {
                InvoiceItemsModel model = new InvoiceItemsModel();
                if (dataTable.Rows.Count > 0)
                {
                                                      
                    return invoiceItem;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}