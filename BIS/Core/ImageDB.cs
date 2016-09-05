using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace BIS.Core
{
    public class ImageDB
    {
       public byte[] ImageToBytes(Image imageIn)
        {
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    imageIn.Save(mStream, imageIn.RawFormat);
                    return mStream.ToArray();
                }
            }
           catch(Exception e)
            {
                return null;
            }
        }

       public Image BytesToImage(byte[] byteArrayIn)
       {
           try
           {
               MemoryStream ms = new MemoryStream(byteArrayIn);
               Image returnImage = Image.FromStream(ms);
               return returnImage;
           }
           catch (Exception e)
           {
               return null;
           }
       }

       public Image setImage(string strImage)
       {
           try
           {
               if (strImage != null)
               {
                   ImageDB image = new ImageDB();
                   return image.BytesToImage(Convert.FromBase64String(strImage));
               }
               else
                   return null;
           }
           catch (Exception e)
           {
               return null;
           }
       }

    }
}
