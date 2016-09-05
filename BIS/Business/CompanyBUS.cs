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
    public class CompanyBUS
    {
        private CompanyDAO companyDAO;

        public CompanyBUS()
        {
            companyDAO = new CompanyDAO();
        }

        public List<CompanyModel> GetCompanyDetails()
        {
            List<CompanyModel> compList = new List<CompanyModel>();

            DataTable dataTable = new DataTable();
            dataTable = companyDAO.returnCompany();


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    CompanyModel compmod = new CompanyModel();
                   
                    compmod.idCompany = Convert.ToInt32(dr["idCompany"].ToString());


                    if (dr["logoCompany"].ToString() != "")
                    {
                        compmod.logoCompany = dr["logoCompany"].ToString();
                    }
                    else
                        compmod.logoCompany = null;

                    if (dr["nameCompany"].ToString() != "")
                    {
                        compmod.nameCompany = dr["nameCompany"].ToString();
                    }
                    else
                        compmod.nameCompany = null;

                    if (dr["iconCompany"].ToString() != "")
                    {
                        compmod.iconCompany = dr["iconCompany"].ToString();
                    }
                    else
                        compmod.iconCompany = null;

                    if (dr["flag"].ToString() != "")
                    {
                        compmod.flag = dr["flag"].ToString();
                    }
                    else
                        compmod.flag = null;

                    compList.Add(compmod);
                }

                return compList;
            }
            else
            {
                return null;
            }
        }
    }


}
