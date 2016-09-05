using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.DAO;
using System.Data;

namespace BIS.Business
{
    public class CanDeleteBUS
    {
        private CanDeleteDAO cdDAO;

        public CanDeleteBUS()
        {
            cdDAO = new CanDeleteDAO();
        }

        public Boolean canDelete(string nameTable, string nameParametar, string valueParametar)
        {
            DataTable dataTable = new DataTable();
            dataTable = cdDAO.canDelete(nameTable,nameParametar,valueParametar);
            Boolean res = false;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    res = true;
                }
            }
            return res;
        }
    }
}
