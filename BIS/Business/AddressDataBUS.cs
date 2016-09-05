using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;

namespace BIS.Business
{
    public class AddressDataBUS
    {
        private AddressDataDAO addDAO;

        public AddressDataBUS()
        {
            addDAO = new AddressDataDAO();
        }

        public DataTable GetAddress(string letterAdress, int indicator, int districtAdress, int housenum)
        {
            try
            {
                return addDAO.GetAddress(letterAdress, indicator, districtAdress, housenum);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
