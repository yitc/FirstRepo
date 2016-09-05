using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;

namespace BIS.DAO
{
    public class AccTaxDAO
    {
        private dbConnection conn;

        public AccTaxDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllTax(string idLang)
        {
            string query = string.Format(@"SELECT idTax,codeTax,descTax, typeTax,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE (CASE WHEN typeTax=1 THEN 'Inclusive' ELSE CASE WHEN typeTax=2 THEN 'Exclusive' ELSE NULL END END) END as nameTax,
                                        numberLedAccount, c.descLedgerAccount as nameAccount
                                        FROM AccTax
                                        LEFT JOIN AccLedgerAccount c ON numberLedAccount = c.numberLedgerAccount
                                        LEFT JOIN STRING" + idLang + @" s ON (CASE WHEN typeTax=1 THEN 'Inclusive' ELSE CASE WHEN typeTax=2 THEN 'Exclusive' ELSE NULL END END) = s.stringKey");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetTaxByID(int idTax)
        {
            string query = string.Format(@"SELECT idTax,codeTax, descTax,typeTax,numberLedAccount  FROM AccTax WHERE idTax = @idTax");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idTax", SqlDbType.Int);
            sqlParameters[0].Value = idTax;

            return conn.executeSelectQuery(query, sqlParameters);
          
        }

        public DataTable GetTaxByCode(string idTax)
        {
            string query = string.Format(@"SELECT idTax,codeTax, descTax,typeTax,numberLedAccount  FROM AccTax WHERE codeTax = @idTax");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idTax", SqlDbType.NVarChar);
            sqlParameters[0].Value = idTax;

            return conn.executeSelectQuery(query, sqlParameters);

        }

        public Boolean Save(AccTaxModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccTax (codeTax,descTax,typeTax,numberLedAccount ) 
                      VALUES(@codeTax, @descTax,@typeTax, @numberLedAccount)");


            SqlParameter[] sqlParameter = new SqlParameter[4];
            sqlParameter[0] = new SqlParameter("@codeTax", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.codeTax;

            sqlParameter[1] = new SqlParameter("@descTax", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.descTax;

            sqlParameter[2] = new SqlParameter("@typeTax", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.typeTax;

            sqlParameter[3] = new SqlParameter("@numberLedAccount", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.numberLedAccount;


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
            sqlParameter[4].Value = conn.GetLastTableID("AccTax")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTax";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(AccTaxModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccTax SET codeTax=@codeTax, descTax = @descTax, typeTax=@typeTax, numberLedAccount=@numberLedAccount WHERE  idTax=@idTax ");


            SqlParameter[] sqlParameter = new SqlParameter[5];

            sqlParameter[0] = new SqlParameter("@idTax", SqlDbType.Int);
            sqlParameter[0].Value = model.idTax;

            sqlParameter[1] = new SqlParameter("@codeTax", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.codeTax;

            sqlParameter[2] = new SqlParameter("@descTax", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.descTax;

            sqlParameter[3] = new SqlParameter("@typeTax", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.typeTax;

            sqlParameter[4] = new SqlParameter("@numberLedAccount", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.numberLedAccount;



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
            sqlParameter[4].Value = model.idTax;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTax";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Delete(int idTax, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE AccTax  WHERE  idTax=@idTax ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTax", SqlDbType.Int);
            sqlParameter[0].Value = idTax;


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
            sqlParameter[4].Value = idTax;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTax";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

    }


}