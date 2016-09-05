using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
    public class GenderBUS
    {
         private GenderDAO genderDAO;

         public GenderBUS()
        {
            genderDAO = new GenderDAO();
        }

        public List<GenderModel> GetAllGenders(string lang)
        {
            DataTable dataTable = new DataTable();
            dataTable = genderDAO.GetGenders(lang);
            List<GenderModel> genders = new List<GenderModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        GenderModel model = new GenderModel();

                        model.idGender = Int32.Parse(dr["idGender"].ToString());

                        if (dr["nameGender"].ToString() != "")
                            model.nameGender = dr["nameGender"].ToString();

                        genders.Add(model);
                    }
                    return genders;
                }
                else
                    return genders;
            }
            else
                return genders;
        }

    }
}
