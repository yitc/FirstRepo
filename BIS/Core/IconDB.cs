using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Core
{
    public class IconDB
    {
        public byte[] IconToBytes(Icon icon)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                icon.Save(ms);
                return ms.ToArray();
            }
        }

        public Icon BytesToIcon(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return new Icon(ms);
            }
        }

        public Icon setIcon(string strIcon)
        {
            try
            {
                if (strIcon != null)
                {
                    IconDB icon = new IconDB();
                    return icon.BytesToIcon(Convert.FromBase64String(strIcon));
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
