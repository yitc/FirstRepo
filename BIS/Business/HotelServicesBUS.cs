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
   public class HotelServicesBUS
    {
       private HotelServicesDAO hotelServicesDAO;
         public HotelServicesBUS()
        {
            hotelServicesDAO = new HotelServicesDAO();
        }

       public List<IModel> GetAllHotelServices()
       {
           DataTable dataTable = new DataTable();
           dataTable = hotelServicesDAO.GetAllHotelservices();

           List<IModel> modelHotel = new List<IModel>();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   foreach (DataRow dr in dataTable.Rows)
                   {
                       HotelServicesModel model = new HotelServicesModel();

                       model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());
                       model.nameHotelService = dr["nameHotelService"].ToString();



                       modelHotel.Add(model);

                   }
                   return modelHotel;
               }
               else
                   return null;
           }
           else
               return null;
       }

       public List<HotelServicesModel> GetAllHotelServicesDropDown()
       {
           DataTable dataTable = new DataTable();
           dataTable = hotelServicesDAO.GetAllHotelservices();

           List<HotelServicesModel> modelHotel = new List<HotelServicesModel>();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   HotelServicesModel model = new HotelServicesModel();
                   model.idHotelService = 0;
                   model.nameHotelService = "";
                   modelHotel.Add(model);

                   foreach (DataRow dr in dataTable.Rows)
                   {
                       model = new HotelServicesModel();

                       model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());
                       model.nameHotelService = dr["nameHotelService"].ToString();

                       modelHotel.Add(model);

                   }
                   return modelHotel;
               }
               else
                   return null;
           }
           else
               return null;
       }

       public HotelServicesModel GetHotelServicesById(int idservice)
       {
           DataTable dataTable = new DataTable();
           dataTable = hotelServicesDAO.GetHotelservicesById(idservice);

           HotelServicesModel model = new HotelServicesModel();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   foreach (DataRow dr in dataTable.Rows)
                   {
                        model = new HotelServicesModel();

                       model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());
                       model.nameHotelService = dr["nameHotelService"].ToString();



                      // modelHotel.Add(model);

                   }
                   return model;
               }
               else
                   return null;
           }
           else
               return null;
       }
       public bool Save(int idHotelService, string nameHotelService, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = hotelServicesDAO.Save(idHotelService, nameHotelService,  nameForm,  idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

       public bool Update(int idHotelService, string nameHotelService, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = hotelServicesDAO.Update(idHotelService, nameHotelService, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

       public List<HotelServicesModel> LastID()
       {
           DataTable dataTable = new DataTable();
           dataTable = hotelServicesDAO.LastID();

           List<HotelServicesModel> modelHotel = new List<HotelServicesModel>();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   foreach (DataRow dr in dataTable.Rows)
                   {
                       HotelServicesModel model = new HotelServicesModel();

                       model.idHotelService = Int32.Parse(dr["idHotelService"].ToString());
                     


                       modelHotel.Add(model);

                   }
                   return modelHotel;
               }
               else
                   return null;
           }
           else
               return null;
       }

       

      
       public int checkIsInArrangemnet(int idAgeCategory)
       {
           int num = 0;
           DataTable dataTable = new DataTable();
           dataTable = hotelServicesDAO.checkIsInArrangemnet(idAgeCategory);

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   num = 1;
               }

           }
           return num;
       }
       public bool DeleteHotelServicesSript(int idAgeCategory, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = hotelServicesDAO.DeleteHotelServicesSript(idAgeCategory, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }
    }
}
