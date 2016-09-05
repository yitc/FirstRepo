using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
    using System.Data;
    using System.Data.SqlClient;
    using BIS.DAO;
    using BIS.Model;

    public class PersonLivingBUS
    {

        private PersonLivingDAO livingDAO;

        public PersonLivingBUS()
        {
            livingDAO = new PersonLivingDAO();
        }

        public List<PersonLivingModel> GetAllLiving()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = livingDAO.GetAllLiving();

                List<PersonLivingModel> living = new List<PersonLivingModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            PersonLivingModel model = new PersonLivingModel();

                            model.idLiving = Int32.Parse(dr["idLiving"].ToString());
                            model.nameLiving = dr["nameLiving"].ToString();


                            living.Add(model);
                        }
                        return living;
                    }
                    else
                        return living;
                }
                else
                    return living;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
