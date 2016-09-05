using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;
using System.Data.SqlTypes;

namespace BIS.DAO
{
    public class AccTaxValidityDAO
    {
        private dbConnection conn;
        public AccTaxValidityDAO()
        {
            conn = new dbConnection();

        }

        //public DataTable GetTaxValidity(string codeTax, DateTime start)
             public DataTable GetTaxValidity(string codeTax)
        {
            string query = string.Format(@"SELECT idTaxValidity,codeTax,startDate,percentTax,endDate
              FROM AccTaxValidity
                WHERE codeTax = '" + codeTax.ToString() + "' ");
            // and startDate >= '"+start+ "' and endDate isnull 

            return conn.executeSelectQuery(query, null);

        }

             public DataTable GetTaxValidityNew(string codeTax)
             {
                 string query = string.Format(@"SELECT idTaxValidity,codeTax,startDate,percentTax,endDate
              FROM AccTaxValidity
                WHERE codeTax = '" + codeTax.ToString() + "'  and endDate = '0001-01-01'");
                 // and startDate >= '"+start+ "' and endDate isnull 

                 return conn.executeSelectQuery(query, null);

             }
             public DataTable GetTaxValidityById(int idTaxValidity)
             {
                 string query = string.Format(@"SELECT idTaxValidity,codeTax,startDate,percentTax,endDate
              FROM AccTaxValidity
                WHERE idTaxValidity = '" + idTaxValidity.ToString() + "' and endDate = '0001-01-01' ");
                

                 return conn.executeSelectQuery(query, null);

             }
             public DataTable GetTaxValidityByCode(string codeTax)
             {
                 string query = string.Format(@"SELECT idTaxValidity,codeTax,startDate,percentTax,endDate
              FROM AccTaxValidity
                WHERE codeTax = '" + codeTax.ToString() + "' and endDate = '0001-01-01' ");


                 return conn.executeSelectQuery(query, null);

             }

             public bool Save(AccTaxValidityModel tax, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccTaxValidity (codeTax, startDate,percentTax,endDate)
                            VALUES(@codeTax, @startDate, @percentTax, @endDate)");


            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@codeTax", SqlDbType.NVarChar);
            sqlParameter[0].Value = tax.codeTax;

            sqlParameter[1] = new SqlParameter("@startDate", SqlDbType.Date);
            sqlParameter[1].Value = tax.startDate;

            sqlParameter[2] = new SqlParameter("@percentTax", SqlDbType.Decimal);
            sqlParameter[2].Value = tax.percentTax;

            //sqlParameters[3] = new SqlParameter("@endDate", SqlDbType.DateTime);
            //sqlParameters[3].Value = (tax.endDate == null || tax.endDate == DateTime.MinValue) ? SqlDateTime.Null : tax.endDate;

            //sqlParameters[1] = new SqlParameter("@endDate", SqlDbType.DateTime);
            //sqlParameters[1].Value = (tax.endDate == null || tax.endDate == DateTime.MinValue) ? SqlDateTime.Null : tax.endDate;

            sqlParameter[3] = new SqlParameter("@endDate", SqlDbType.Date);
            sqlParameter[3].Value = tax.endDate;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = conn.GetLastTableID("AccTaxValidity") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTaxValidity";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccTaxValidity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

             public bool Update(AccTaxValidityModel tax, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idTaxValidity,codeTax,startDate,percentTax,endDate
                FROM AccTaxValidity
                WHERE idTaxValidity = '" + tax.idTaxValidity.ToString() + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                string query = string.Format(@"UPDATE AccTaxValidity SET  codeTax = @codeTax, 
                startDate = @startDate, percentTax = @percentTax,endDate = @endDate 
                WHERE idTaxValidity = @idTaxValidity ");


                SqlParameter[] sqlParameter = new SqlParameter[5];

                sqlParameter[0] = new SqlParameter("@codeTax", SqlDbType.NVarChar);
                sqlParameter[0].Value = (tax.codeTax == null) ? SqlString.Null : tax.codeTax;

                sqlParameter[1] = new SqlParameter("@startDate", SqlDbType.Date);
                sqlParameter[1].Value = tax.startDate;

                sqlParameter[2] = new SqlParameter("@percentTax", SqlDbType.Decimal);
                sqlParameter[2].Value = tax.percentTax;

                sqlParameter[3] = new SqlParameter("@endDate", SqlDbType.Date);
                sqlParameter[3].Value = tax.endDate;

                //sqlParameters[3] = new SqlParameter("@endDate", SqlDbType.DateTime);
                //sqlParameters[3].Value = (tax.endDate == null || tax.endDate == DateTime.MinValue) ? SqlDateTime.MinValue : tax.endDate;
              
                sqlParameter[4] = new SqlParameter("@idTaxValidity", SqlDbType.Int);
                sqlParameter[4].Value = tax.idTaxValidity;
               // (tax.endDate == null) ? SqlDbType.DateTime : tax.endDate;

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                sqlParameter = new SqlParameter[8];


                sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                sqlParameter[0].Value = nameForm;

                sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                sqlParameter[1].Value = idUser;

                sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                sqlParameter[2].Value = DateTime.Now;

                sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                sqlParameter[3].Value = "U";

                sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                sqlParameter[4].Value = tax.idTaxValidity;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idTaxValidity";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "AccTaxValidity";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);

                return conn.executQueryTransaction(_query, sqlParameters);

            }
            else
            {
                return Save(tax, nameForm, idUser);
            }
        }

             public bool Delete(int idTaxValidity, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccTaxValidity WHERE idTaxValidity = @idTaxValidity");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTaxValidity", SqlDbType.Int);
            sqlParameter[0].Value = idTaxValidity;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idTaxValidity;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTaxValidity";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccTaxValidity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}