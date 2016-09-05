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
    public class AccCreditPayDAO
    {
        private dbConnection conn;
        public AccCreditPayDAO()
        {
            conn = new dbConnection();

        }
         public DataTable GetAllPays()
        {
            string query = string.Format(@" SELECT * FROM AccCreditPay WHERE isAprBook = 0 ");

            return conn.executeSelectQuery(query, null);

        }
         public DataTable GetAllPaysApproved()
         {
             string query = string.Format(@" SELECT * FROM AccCreditPay WHERE isAprBook = 1 and isBooked = 0 ");

             return conn.executeSelectQuery(query, null);

         }
        public DataTable GetInvoicesByTaskEmployee(int employee)
         {
             string query = string.Format(@" SELECT * from  AccCreditPay
                                             INNER JOIN todo b on idTask=b.idToDo  WHERE idEmployee = '" + employee.ToString() + "' and isAprBook = 0 ");

             return conn.executeSelectQuery(query, null);

         }
        public DataTable GetBankPayByDate(DateTime dateval, DateTime dateplus)
        {
            string query = string.Format(@" SELECT *, DATEDIFF (day ,dtValuta ,'" + dateval.ToString("MM/dd/yyyy") + @"' ) as ndays FROM AccCreditPay WHERE isAprBook = 1 and isBooked = 1 and
                        dtValuta >= '" + dateval.ToString("MM/dd/yyyy") + "' and dtValuta <= '" + dateplus.ToString("MM/dd/yyyy") + "'");

           

            return conn.executeSelectQuery(query, null);

        }

        public bool UpdateApproved(AccCreditPayModel model, int status, int user, string nameForm, int idUser)
         {
             List<string> _query = new List<string>();
             List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

             string query = string.Format(@"UPDATE AccCreditPay SET  isApproved=@status,isAprBook =@status, approvedUser=@user  WHERE idCreditPay =@idCreditPay");

             SqlParameter[] sqlParameter = new SqlParameter[3];

             sqlParameter[0] = new SqlParameter("@idCreditPay", SqlDbType.Int);
             sqlParameter[0].Value = model.idCreditPay;

             sqlParameter[1] = new SqlParameter("@status", SqlDbType.Int);
             sqlParameter[1].Value = status;

             sqlParameter[2] = new SqlParameter("@user", SqlDbType.Int);
             sqlParameter[2].Value = user;

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
             sqlParameter[4].Value = model.idCreditPay;

             sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
             sqlParameter[5].Value = "idCreditPay";

             sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
             sqlParameter[6].Value = "AccCreditPay";

             sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
             sqlParameter[7].Value = "Update approved";

             _query.Add(query);
             sqlParameters.Add(sqlParameter);


             return conn.executQueryTransaction(_query, sqlParameters);
         }

        public bool Update(AccCreditPayModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccCreditPay SET   dtItem=@dtItem,dtValuta=@dtValuta,accNumber=@accNumber,idClient=@idClient,
                              idContPers=@idContPers,account=@account,invoiceNr=@invoiceNr,inkopNr=@inkopNr,
                              iban=@iban,descItem=@descItem,amountC=@amountC,idBtw=@idBtw, amountInCurr=@amountInCurr, currency=@currency,
                              cost=@cost,project=@project,isApproved=@isApproved,isBooked=@isBooked,isSent=@isSent,dtSent=@dtSent,
                              namefile=@namefile, approvedUser=@approvedUser,createUser=@createUser,dtCreation=@dtCreation,payIban=@payIban,isSelected=@isSelected,
                              idDocument=@idDocument, idOption=@idOption,amountD=@amountD, paydays=@paydays,idTask=@idTask,isAprBook=@isAprBook,
                              userCreated=@userCreated,dtCreated=@dtCreated,userModified=@userModified,dtModified=@dtModified
                              WHERE idCreditPay=@idCreditPay");

            SqlParameter[] sqlParameter = new SqlParameter[37];

            sqlParameter[0] = new SqlParameter("@dtItem", SqlDbType.DateTime);
            sqlParameter[0].Value = model.dtItem;
            sqlParameter[1] = new SqlParameter("@dtValuta", SqlDbType.DateTime);
            sqlParameter[1].Value = model.dtValuta;
            sqlParameter[2] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.accNumber;
            sqlParameter[3] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[3].Value = model.idClient;
            sqlParameter[4] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[4].Value = model.idContPers;
            sqlParameter[5] = new SqlParameter("@account", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.account;
            sqlParameter[6] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
            sqlParameter[6].Value = model.invoiceNr;
            sqlParameter[7] = new SqlParameter("@inkopNr", SqlDbType.NVarChar);
            sqlParameter[7].Value = model.inkopNr;
            sqlParameter[8] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[8].Value = model.iban;
            sqlParameter[9] = new SqlParameter("@descItem", SqlDbType.NVarChar);
            sqlParameter[9].Value = model.descItem;
            sqlParameter[10] = new SqlParameter("@amountC", SqlDbType.Decimal);
            sqlParameter[10].Value = model.amountC;
            sqlParameter[11] = new SqlParameter("@idBtw", SqlDbType.Int);
            sqlParameter[11].Value = model.idBtw;
            sqlParameter[12] = new SqlParameter("@amountInCurr", SqlDbType.Decimal);
            sqlParameter[12].Value = model.amountInCurr;
            sqlParameter[13] = new SqlParameter("@currency", SqlDbType.NVarChar);
            sqlParameter[13].Value = model.currency;
            sqlParameter[14] = new SqlParameter("@cost", SqlDbType.NVarChar);
            sqlParameter[14].Value = model.cost;
            sqlParameter[15] = new SqlParameter("@project", SqlDbType.NVarChar);
            sqlParameter[15].Value = model.project;
            sqlParameter[16] = new SqlParameter("@isApproved", SqlDbType.Bit);
            sqlParameter[16].Value = model.isApproved;
            sqlParameter[17] = new SqlParameter("@isBooked", SqlDbType.Bit);
            sqlParameter[17].Value = model.isBooked;
            sqlParameter[18] = new SqlParameter("@isSent", SqlDbType.Bit);
            sqlParameter[18].Value = model.isSent;
            sqlParameter[19] = new SqlParameter("@dtSent", SqlDbType.DateTime);
            sqlParameter[19].Value = model.dtSent;
            sqlParameter[20] = new SqlParameter("@namefile", SqlDbType.NVarChar);
            sqlParameter[20].Value = model.namefile;
            sqlParameter[21] = new SqlParameter("@approvedUser", SqlDbType.Int);
            sqlParameter[21].Value = model.approvedUser;
            sqlParameter[22] = new SqlParameter("@createUser", SqlDbType.Int);
            sqlParameter[22].Value = model.createUser;
            sqlParameter[23] = new SqlParameter("@dtCreation", SqlDbType.DateTime);
            sqlParameter[23].Value = model.dtCreation;
            sqlParameter[24] = new SqlParameter("@payIban", SqlDbType.NVarChar);
            sqlParameter[24].Value = model.payIban;
            sqlParameter[25] = new SqlParameter("@isSelected", SqlDbType.Bit);
            sqlParameter[25].Value = model.isSelected;
            sqlParameter[26] = new SqlParameter("@idCreditPay", SqlDbType.Int);
            sqlParameter[26].Value = model.idCreditPay;
            sqlParameter[27] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameter[27].Value = model.idDocument;
            sqlParameter[28] = new SqlParameter("@idOption", SqlDbType.Int);
            sqlParameter[28].Value = model.idOption;
            sqlParameter[29] = new SqlParameter("@amountD", SqlDbType.Decimal);
            sqlParameter[29].Value = model.amountD;
            sqlParameter[30] = new SqlParameter("@paydays", SqlDbType.Int);
            sqlParameter[30].Value = model.paydays;
            sqlParameter[31] = new SqlParameter("@idTask", SqlDbType.Int);
            sqlParameter[31].Value = model.idTask;
            sqlParameter[32] = new SqlParameter("@isAprBook", SqlDbType.Bit);
            sqlParameter[32].Value = model.isAprBook;
            sqlParameter[33] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[33].Value = model.userCreated;

            sqlParameter[34] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[34].Value = model.dtCreated;

            sqlParameter[35] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[35].Value = model.userModified;

            sqlParameter[36] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[36].Value = DateTime.Now; // model.dtModified;

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
            sqlParameter[4].Value = model.idCreditPay;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCreditPay";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditPay";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);


        }

        public int Save(AccCreditPayModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccCreditPay  (dtItem,dtValuta,accNumber,idClient,idContPers,account,invoiceNr,inkopNr,
                              iban,descItem,amountC,idBtw, amountInCurr, currency,cost,project,isApproved,isBooked,isSent,dtSent,
                               namefile, approvedUser,createUser,dtCreation,payIban,isSelected,idDocument,idOption, amountD,paydays,idTask,isAprBook,
                                 userCreated, dtCreated, userModified, dtModified)
                             VALUES (@dtItem,@dtValuta,@accNumber,@idClient,@idContPers,@account,@invoiceNr,@inkopNr,
                              @iban,@descItem,@amountC,@idBtw, @amountInCurr, @currency,@cost,@project,@isApproved,@isBooked,@isSent,@dtSent,
                               @namefile, @approvedUser,@createUser,@dtCreation,@payIban,@isSelected, @idDocument,@idOption,@amountD,@paydays,
                                @idTask,@isAprBook, @userCreated, @dtCreated, @userModified, @dtModified);SELECT SCOPE_IDENTITY();");



            SqlParameter[] sqlParameter = new SqlParameter[36];

            sqlParameter[0] = new SqlParameter("@dtItem", SqlDbType.DateTime);
            sqlParameter[0].Value = model.dtItem ;
            sqlParameter[1] = new SqlParameter("@dtValuta", SqlDbType.DateTime);
            sqlParameter[1].Value = model.dtValuta;
            sqlParameter[2] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.accNumber;
            sqlParameter[3] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[3].Value = model.idClient;
            sqlParameter[4] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[4].Value = model.idContPers;
            sqlParameter[5] = new SqlParameter("@account", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.account;
            sqlParameter[6] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
            sqlParameter[6].Value = model.invoiceNr;
            sqlParameter[7] = new SqlParameter("@inkopNr", SqlDbType.NVarChar);
            sqlParameter[7].Value = model.inkopNr;
            sqlParameter[8] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[8].Value = model.iban;
            sqlParameter[9] = new SqlParameter("@descItem", SqlDbType.NVarChar);
            sqlParameter[9].Value = model.descItem;
            sqlParameter[10] = new SqlParameter("@amountC", SqlDbType.Decimal);
            sqlParameter[10].Value = model.amountC;
            sqlParameter[11] = new SqlParameter("@idBtw", SqlDbType.Int);
            sqlParameter[11].Value = model.idBtw;
            sqlParameter[12] = new SqlParameter("@amountInCurr", SqlDbType.Decimal);
            sqlParameter[12].Value = model.amountInCurr;
            sqlParameter[13] = new SqlParameter("@currency", SqlDbType.NVarChar);
            sqlParameter[13].Value = model.currency;
            sqlParameter[14] = new SqlParameter("@cost", SqlDbType.NVarChar);
            sqlParameter[14].Value = model.cost;
            sqlParameter[15] = new SqlParameter("@project", SqlDbType.NVarChar);
            sqlParameter[15].Value = model.project;
            sqlParameter[16] = new SqlParameter("@isApproved", SqlDbType.Bit);
            sqlParameter[16].Value = model.isApproved;
            sqlParameter[17] = new SqlParameter("@isBooked", SqlDbType.Bit);
            sqlParameter[17].Value = model.isBooked;
            sqlParameter[18] = new SqlParameter("@isSent", SqlDbType.Bit);
            sqlParameter[18].Value = model.isSent;
            sqlParameter[19] = new SqlParameter("@dtSent", SqlDbType.DateTime);
            sqlParameter[19].Value = model.dtSent;
            sqlParameter[20] = new SqlParameter("@namefile", SqlDbType.NVarChar);
            sqlParameter[20].Value = model.namefile;
            sqlParameter[21] = new SqlParameter("@approvedUser", SqlDbType.Int);
            sqlParameter[21].Value = model.approvedUser;
            sqlParameter[22] = new SqlParameter("@createUser", SqlDbType.Int);
            sqlParameter[22].Value = model.createUser;
            sqlParameter[23] = new SqlParameter("@dtCreation", SqlDbType.DateTime);
            sqlParameter[23].Value = model.dtCreation;
            sqlParameter[24] = new SqlParameter("@payIban", SqlDbType.NVarChar);
            sqlParameter[24].Value = model.payIban;
            sqlParameter[25] = new SqlParameter("@isSelected", SqlDbType.Bit);
            sqlParameter[25].Value = model.isSelected;
            sqlParameter[26] = new SqlParameter("@idDocument", SqlDbType.Int);
            sqlParameter[26].Value = model.idDocument;
            sqlParameter[27] = new SqlParameter("@idOption", SqlDbType.Int);
            sqlParameter[27].Value = model.idOption;
            sqlParameter[28] = new SqlParameter("@amountD", SqlDbType.Decimal);
            sqlParameter[28].Value = model.amountD;
            sqlParameter[29] = new SqlParameter("@paydays", SqlDbType.Int);
            sqlParameter[29].Value = model.paydays;
            sqlParameter[30] = new SqlParameter("@idTask", SqlDbType.Int);
            sqlParameter[30].Value = model.idTask;
            sqlParameter[31] = new SqlParameter("@isAprBook", SqlDbType.Bit);
            sqlParameter[31].Value = model.isAprBook;
            sqlParameter[32] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[32].Value = model.userCreated;

            sqlParameter[33] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[33].Value = DateTime.Now; //model.dtCreated;

            sqlParameter[34] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[34].Value = model.userModified;

            sqlParameter[35] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[35].Value = model.dtModified;


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
            sqlParameter[4].Value = conn.GetLastTableID("AccCreditPay")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCreditPay";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditPay";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);
           // return conn.executeInsertQuery(query, sqlParameters);


        }

        public bool Delete(int id,string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AccCreditPay WHERE idCreditPay = @id");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameter[0].Value = id;

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
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCreditPay";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditPay";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }



    }

}
       