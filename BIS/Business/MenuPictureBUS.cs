using BIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
  public class MenuPictureBUS
    {
       private MenuPictureDAO menuDAO;

       public MenuPictureBUS()
        {
            menuDAO = new MenuPictureDAO();
        }

       public object GetImage(int idMenu)
       {
           object retval = null;
           try
           {
               retval = menuDAO.GetImage(idMenu);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

       public object GetImageNew(int idMenu)
       {
           object retval = null;
           try
           {
               retval = menuDAO.GetImageNew(idMenu);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

       public object GetImageDelete(int idMenu)
       {
           object retval = null;
           try
           {
               retval = menuDAO.GetImageDelete(idMenu);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }

       public bool UpdateImage(int idMenu,string imageMenu, string imageNew, string imageDelete, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {
               retval = menuDAO.UpdateImage(idMenu, imageMenu,  imageNew,  imageDelete, nameForm, idUser);

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return retval;
       }
    }
}
