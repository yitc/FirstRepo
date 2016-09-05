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
    public class TypesTelBUS
    {
        private TypesTelDAO typesTelDAO;

        public TypesTelBUS()
        {
            typesTelDAO = new TypesTelDAO();
        }

        public List<TypesTelModel> GetAllTypeTel(string idLang)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = typesTelDAO.GetAllTypeTel(idLang);

                List<TypesTelModel> typestel = new List<TypesTelModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TypesTelModel model = new TypesTelModel();

                            model.idTelType = Int32.Parse(dr["idTelType"].ToString());
                            model.nameTelType = dr["nameTelType"].ToString();
                          

                            typestel.Add(model);
                        }
                        return typestel;
                    }
                    else
                        return typestel;
                }
                else
                    return typestel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
