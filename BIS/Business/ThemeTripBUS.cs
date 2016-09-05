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
 public  class ThemeTripBUS
    {
     private ThemeTripDAO themeTripDAO;

           public ThemeTripBUS()
        {
            themeTripDAO = new ThemeTripDAO();
        }
           public bool Save(int idThemeTrip, string nameThemeTrip, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = themeTripDAO.Save(idThemeTrip, nameThemeTrip,nameForm,idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

    
           public List<IModel> GetAllThemeTrip()
           {
               try
               {
                   DataTable dataTable = new DataTable();
                   dataTable = themeTripDAO.GetAllThemeTrip();
                   List<IModel> theme = new List<IModel>();

                   if (dataTable != null)
                   {
                       if (dataTable.Rows.Count > 0)
                       {
                           foreach (DataRow dr in dataTable.Rows)
                           {
                               ThemeTripModel model = new ThemeTripModel();

                               model.idThemeTrip = Int32.Parse(dr["idThemeTrip"].ToString());
                               model.nameThemeTrip = (dr["nameThemeTrip"].ToString());


                               theme.Add(model);
                           }
                           return theme;
                       }
                       else
                           return theme;
                   }
                   else
                       return theme;
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message);
               }
           }
           public bool Delete(int idThemeTrip, string nameForm, int idUser)
           {
               bool retval = false;
               try
               {

                   retval = themeTripDAO.Delete(idThemeTrip, nameForm, idUser);

               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message);
               }

               return retval;
           }
           public bool Update(int idThemeTrip, string nameThemeTrip, string nameForm, int idUser)
           {
               bool retval = false;
               try
               {

                   retval = themeTripDAO.Update(idThemeTrip, nameThemeTrip, nameForm, idUser);

               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message);
               }

               return retval;
           }
     //Novo
           public List<ThemeTripModel> isInTheme(int idThemeTrip)
           {
               try
               {
                   DataTable dataTable = new DataTable();
                   dataTable = themeTripDAO.isInTheme(idThemeTrip);
                   List<ThemeTripModel> theme = new List<ThemeTripModel>();

                   if (dataTable != null)
                   {
                       if (dataTable.Rows.Count > 0)
                       {
                           foreach (DataRow dr in dataTable.Rows)
                           {
                               ThemeTripModel model = new ThemeTripModel();

                               model.idThemeTrip = Int32.Parse(dr["idThemeTrip"].ToString());
                               model.nameThemeTrip = (dr["nameThemeTrip"].ToString());


                               theme.Add(model);
                           }
                           return theme;
                       }
                       else
                           return theme;
                   }
                   else
                       return theme;
               }
               catch (Exception ex)
               {
                   throw new Exception(ex.Message);
               }
           }
    }
}
