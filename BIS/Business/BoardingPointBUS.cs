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
  public class BoardingPointBUS
    {
      private BoardingPointDAO boardingPointDAO;

       public BoardingPointBUS()
        {
            boardingPointDAO = new BoardingPointDAO();
        }
       public bool Save(int idBoardingPoint, string nameBoardingPoint, string addressBoardingPoint, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = boardingPointDAO.Save(idBoardingPoint, nameBoardingPoint, addressBoardingPoint,nameForm,idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }
       public List<IModel> GetAll()
       {
           try
           {
               DataTable dataTable = new DataTable();
               dataTable = boardingPointDAO.GetAll();
               List<IModel> theme = new List<IModel>();

               if (dataTable != null)
               {
                   if (dataTable.Rows.Count > 0)
                   {
                       foreach (DataRow dr in dataTable.Rows)
                       {
                           BoardingPointModel model = new BoardingPointModel();

                           model.idBoardingPoint = Int32.Parse(dr["idBoardingPoint"].ToString());
                           model.addressBoardingPoint = (dr["addressBoardingPoint"].ToString());
                           model.nameBoardingPoint = (dr["nameBoardingPoint"].ToString());


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
       public bool Delete(int idBoardingPoint, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = boardingPointDAO.Delete(idBoardingPoint, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }
       public bool Update(int idBoardingPoint, string nameBoardingPoint, string addressBoardingPoint, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {

               retval = boardingPointDAO.Update(idBoardingPoint, nameBoardingPoint, addressBoardingPoint, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }
      // NOVO
       public List<BoardingPointModel> isInBoarding(int idBoardingPoint)
       {
           try
           {
               DataTable dataTable = new DataTable();
               dataTable = boardingPointDAO.isInBoarding(idBoardingPoint);
               List<BoardingPointModel> theme = new List<BoardingPointModel>();

               if (dataTable != null)
               {
                   if (dataTable.Rows.Count > 0)
                   {
                       foreach (DataRow dr in dataTable.Rows)
                       {
                           BoardingPointModel model = new BoardingPointModel();

                           model.idBoardingPoint = Int32.Parse(dr["idBoardingPoint"].ToString());
                           model.nameBoardingPoint = (dr["nameBoardingPoint"].ToString());


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
