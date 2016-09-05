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
    public class TypesEmailBUS
    {
        private TypesEmailDAO TypesEmailDAO;

        public TypesEmailBUS()
        {
            TypesEmailDAO = new TypesEmailDAO();
        }

        public List<TypesEmailModel> GetAllTypeEmail(string idLang)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = TypesEmailDAO.GetAllTypesEmail(idLang);

                List<TypesEmailModel> typesemail = new List<TypesEmailModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TypesEmailModel model = new TypesEmailModel();

                            model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());
                            model.nameEmailType = dr["nameEmailType"].ToString();


                            typesemail.Add(model);
                        }
                        return typesemail;
                    }
                    else
                        return typesemail;
                }
                else
                    return typesemail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
        
}
