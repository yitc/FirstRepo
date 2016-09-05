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
    public class ArrangementInvoicePriceDAO
    {
        private dbConnection conn;

        public ArrangementInvoicePriceDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetArrangementById(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement,idArticle,descriptionArticle, nrArticle,purchasePrice,
                                        purchasePriceTotal,sellingPrice, calculation, isExtra, isOption
                                        FROM ArrangementInvoicePrice WHERE idArrangement = @idArrangement");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

           
            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetInvoicePrice(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement,idArticle,descriptionArticle, nrArticle,purchasePrice,
                                        purchasePriceTotal,sellingPrice, calculation, isExtra, isOption
                                        FROM ArrangementInvoicePrice WHERE idArrangement = @idArrangement and calculation= 1");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetInvoicePriceItemsOption(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement,idArticle,descriptionArticle, nrArticle,purchasePrice,
                                        purchasePriceTotal,sellingPrice, calculation, isExtra, isOption
                                        FROM ArrangementInvoicePrice WHERE idArrangement = @idArrangement and isExtra = 0 and isOption = 1");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetInvoicePriceItemsExtra(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement,idArticle,descriptionArticle, nrArticle,purchasePrice,
                                        purchasePriceTotal,sellingPrice, calculation, isExtra, isOption
                                        FROM ArrangementInvoicePrice WHERE idArrangement = @idArrangement and isExtra= 1");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }


        public Boolean Save(ArrangementInvoicePriceModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementInvoicePrice (idArrangement,idArticle,descriptionArticle,nrArticle,
                            purchasePrice,purchasePriceTotal,sellingPrice, calculation, isExtra, isOption ) 
                      VALUES(@idArrangement,@idArticle,@descriptionArticle,@nrArticle,
                            @purchasePrice,@purchasePriceTotal,@sellingPrice, @calculation, @isExtra, @isOption ) ");


            SqlParameter[] sqlParameter = new SqlParameter[10];
            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = model.idArrangement;

            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.idArticle;

            sqlParameter[2] = new SqlParameter("@descriptionArticle", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.descriptionArticle;

            sqlParameter[3] = new SqlParameter("@nrArticle", SqlDbType.Int);
            sqlParameter[3].Value = model.nrArticle;

            sqlParameter[4] = new SqlParameter("@purchasePrice", SqlDbType.Decimal);
            sqlParameter[4].Value = model.purchasePrice;

            sqlParameter[5] = new SqlParameter("@purchasePriceTotal", SqlDbType.Decimal);
            sqlParameter[5].Value = model.purchasePriceTotal;

            sqlParameter[6] = new SqlParameter("@sellingPrice", SqlDbType.Decimal);
            sqlParameter[6].Value = model.sellingPrice;

            sqlParameter[7] = new SqlParameter("@calculation", SqlDbType.Bit);
            sqlParameter[7].Value = model.calculation;

            sqlParameter[8] = new SqlParameter("@isExtra", SqlDbType.Bit);
            sqlParameter[8].Value = model.isExtra;

            sqlParameter[9] = new SqlParameter("@isOption", SqlDbType.Bit);
            sqlParameter[9].Value = model.isOption;

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
            sqlParameter[4].Value = model.idArrangement.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInvoicePrice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

       
       
        public Boolean Delete(int idArrangement, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE ArrangementInvoicePrice  WHERE  idArrangement=@idArrangement ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;


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
            sqlParameter[4].Value = idArrangement.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementInvoicePrice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public DataTable GetSellingByIdArrangement(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement,idArticle,descriptionArticle, nrArticle,purchasePrice,
                                        purchasePriceTotal,sellingPrice, calculation, isExtra, isOption
                                        FROM ArrangementInvoicePrice WHERE idArrangement ='" + idArrangement + "'  and idArticle='Reis Pakket'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }

    }
}