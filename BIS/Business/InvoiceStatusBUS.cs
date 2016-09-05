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
    public class InvoiceStatusBUS
    {
        private InvoiceStatusDAO invoiceStatusDAO;

        public InvoiceStatusBUS()
        {
            invoiceStatusDAO = new InvoiceStatusDAO();
        }

        public bool Save(InvoiceStatusModel invoiceStatus)
        {
            bool retval;
            try
            {
                retval = invoiceStatusDAO.Save(invoiceStatus);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public Boolean Update(InvoiceStatusModel invoiceStatus)
        {
            bool retval = false;
            try
            {
                retval = invoiceStatusDAO.Update(invoiceStatus);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;

        }

        public List<InvoiceStatusModel> GetAllInvoiceStatus()
        {
            List<InvoiceStatusModel> compList = new List<InvoiceStatusModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceStatusDAO.GetAllInvoiceStatus();

            if(dataTable !=null)
            {
                foreach(DataRow dr in dataTable.Rows)
                {
                    InvoiceStatusModel model = new InvoiceStatusModel();

                    model.idInvoiceStatus = Int32.Parse (dr["idInvoiceStatus"].ToString());
                    model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();

                    compList.Add(model);
                }
                return compList;
            }
            else
            {
                return null;
            }
        }

        public List<InvoiceStatusModel> GeInvoiceStatus(int id)
        {
            List<InvoiceStatusModel> compList = new List<InvoiceStatusModel>();

            DataTable dataTable = new DataTable();
            dataTable = invoiceStatusDAO.GeInvoiceStatus(id);

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    InvoiceStatusModel model = new InvoiceStatusModel();

                    model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                    model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();

                    compList.Add(model);
                }
                return compList;
            }
            else
            {
                return null;
            }
        }
        public InvoiceStatusModel GetInvoiceStatusByID(string idInvoiceStatus)
        {
            DataTable dataTable = new DataTable();
            dataTable = invoiceStatusDAO.GetInvoiceStatusByID(idInvoiceStatus);
            InvoiceStatusModel invoiceStatus = new InvoiceStatusModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    InvoiceStatusModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new InvoiceStatusModel();

                        model.idInvoiceStatus = Int32.Parse(dr["idInvoiceStatus"].ToString());
                        model.descInvoiceStatus = dr["descInvoiceStatus"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}