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
    public class AccCreditOptionBUS
    {
        private AccCreditOptionDAO accCreditOptionDAO;

        public AccCreditOptionBUS()
        {
            accCreditOptionDAO = new AccCreditOptionDAO();
        }

        public List<IModel> GetAllOptions()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = accCreditOptionDAO.GetAllOptions();
                List<IModel> status = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AccCreditOptionModel model = new AccCreditOptionModel();

                            if (dr["idOption"].ToString() != "")
                            {
                                model.idOption = Int32.Parse(dr["idOption"].ToString());
                            }

                            model.descriptionOption = dr["descriptionOption"].ToString();

                            status.Add(model);
                        }
                        return status;
                    }
                    else
                        return status;
                }
                else
                    return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

     
    }


}