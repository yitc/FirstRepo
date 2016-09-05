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
  public  class ProvincesBUS
    {
       private ProvincesDAO provinceDAO;

       public ProvincesBUS()
        {
            provinceDAO = new ProvincesDAO();
        }

       public List<ProvincesModel> GetAllProvinces(int idCountry)
       {
           try
           {
               DataTable dataTable = new DataTable();
               dataTable = provinceDAO.GetAllProvinces(idCountry);
               List<ProvincesModel> province = new List<ProvincesModel>();

               if (dataTable != null)
               {
                   if (dataTable.Rows.Count > 0)
                   {
                       foreach (DataRow dr in dataTable.Rows)
                       {
                           ProvincesModel model = new ProvincesModel();

                           model.idProvinces = Int32.Parse(dr["idProvinces"].ToString());
                           model.codeProvinces = dr["codeProvinces"].ToString();
                           model.nameProvinces = dr["nameProvinces"].ToString();
                           //model.nameCountry = dr["nameCountry"].ToString();
                           model.idCountry = idCountry;

                           province.Add(model);
                       }
                       return province;
                   }
                   else
                       return province;
               }
               else
                   return province;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public bool Insert(int idProvince, string codeProvinces, int idCountry, string nameProvinces, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = provinceDAO.Insert(idProvince,codeProvinces, idCountry, nameProvinces, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

       public bool Delete(int idProvinces, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = provinceDAO.Delete(idProvinces, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

       public bool Update(int idProvinces, string codeProvinces, string nameProvinces, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = provinceDAO.Update(idProvinces, codeProvinces, nameProvinces, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }
    }
}
