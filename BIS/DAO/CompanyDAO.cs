using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
    public class CompanyDAO
    {
        private dbConnection conn;

        public CompanyDAO()
        {
            conn = new dbConnection();
        }
        public DataTable returnCompany()
        {
            string query = string.Format(@"SELECT idCompany, logoCompany, nameCompany, iconCompany,flag
                FROM Company");

            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public void updateImages()
        {
            try
            {
                Image i = Image.FromFile("C:\\D\\ERPLepotica\\Images\\Persoon_meni_dark.png");
                ImageDB ii = new ImageDB();
                byte[] bb = ii.ImageToBytes(i);

                string query = string.Format(@"UPDATE Menu set imageMenu = @imageMenu
	        	WHERE idMenu = '14'");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@imageMenu", SqlDbType.VarChar);
                sqlParameters[0].Value = Convert.ToString(Convert.ToBase64String(bb));
                conn.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception e)
            {

            }
        }

        public void updateIcons()
        {
            try
            {
                Icon i = System.Drawing.Icon.ExtractAssociatedIcon(@"C:\Users\neta.cebedzic\Desktop\ERPLepotica\Images\wielewall.ico");
                IconDB icon = new IconDB();
                byte[] bb = icon.IconToBytes(i);

                string query = string.Format(@"UPDATE Company set iconCompany = @iconCompany
	        	WHERE idCompany = '2'");

                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@iconCompany", SqlDbType.VarChar);
                sqlParameters[0].Value = Convert.ToString(Convert.ToBase64String(bb));
                conn.executeUpdateQuery(query, sqlParameters);
            }
            catch (Exception e)
            {

            }
        }
    }
}
