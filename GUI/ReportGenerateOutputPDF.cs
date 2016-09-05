using Microsoft.Reporting.WinForms;
using System;
using System.IO;

namespace GUI
{
    class ReportGenerateOutputPDF
    {
        private Warning[] warnings;
        private string[] streamids;
        private string mimeType;
        private string encoding;
        private string filenameExtension;
        string nameRep = "";

        //public void GenerateOutputPDF(LocalReport rpt, string name)
        //{
        //    nameRep = name;
        //    if (nameRep == "")
        //        nameRep = "output.pdf";
        //    byte[] bytes = null;
        //    try
        //    {
        //        bytes = rpt.Render(
        //                      "PDF", null, out mimeType, out encoding, out filenameExtension,
        //                      out streamids, out warnings);
        //    }
        //    catch(Exception e)
        //    {

        //    }
        //    using (FileStream fs = new FileStream(nameRep, FileMode.Create))
        //    {
        //        fs.Write(bytes, 0, bytes.Length);
        //    }


        //}

        public void GenerateOutputPDF(LocalReport rpt, string name)
        {
            nameRep = name;
            if (nameRep == "")
                nameRep = "output.pdf";
            byte[] bytes = null;
            try
            {
                bytes = rpt.Render(
                              "PDF", null, out mimeType, out encoding, out filenameExtension,
                              out streamids, out warnings);

                using (FileStream fs = new FileStream(nameRep, FileMode.Create))
                {

                    fs.Write(bytes, 0, bytes.Length);
                }

            }


            catch (Exception e)
            {

            }


        }
    }
}