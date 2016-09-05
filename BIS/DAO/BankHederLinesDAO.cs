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
    public class BankHederLinesDAO
    {
        private dbConnection conn;
        public BankHederLinesDAO()
        {
            conn = new dbConnection();

        }




        public bool Save(BankHederModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string query = string.Format(@"INSERT INTO  BankHeder ( entryDate,statementNo,accountNumber, debcrePrevius, dateStatPrevius,amountPrevius,
                                        debcreEnd,dateEnd,amountEnd )
                                 Values ( @entryDate,@statementNo,@accountNumber, @debcrePrevius,@dateStatPrevius,@amountPrevius,
                                        @debcreEnd,@dateEnd,@amountEnd )");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@statementNo", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.statementNo;

            sqlParameter[1] = new SqlParameter("@accountNumber", SqlDbType.NVarChar);
            sqlParameter[1].Value = linemodel.accountNumber;

            sqlParameter[2] = new SqlParameter("@debcrePrevius", SqlDbType.NVarChar);
            sqlParameter[2].Value = linemodel.debcrePrevius;

            sqlParameter[3] = new SqlParameter("@entryDate", SqlDbType.Date);
            sqlParameter[3].Value = linemodel.entryDate ;

            sqlParameter[4] = new SqlParameter("@dateStatPrevius", SqlDbType.Date);
            sqlParameter[4].Value = linemodel.dateStatPrevius  ;

            sqlParameter[5] = new SqlParameter("@amountPrevius", SqlDbType.Decimal);
            sqlParameter[5].Value = linemodel.amountPrevius ;

            sqlParameter[6] = new SqlParameter("@debcreEnd", SqlDbType.NVarChar);
            sqlParameter[6].Value = linemodel.debcreEnd;

            sqlParameter[7] = new SqlParameter("@dateEnd", SqlDbType.Date);
            sqlParameter[7].Value = linemodel.dateEnd;

            sqlParameter[8] = new SqlParameter("@amountEnd", SqlDbType.Decimal);
            sqlParameter[8].Value = linemodel.amountEnd;


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
            sqlParameter[4].Value = conn.GetLastTableID("BankHeder")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idBankHeder";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BankHeder";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }


        public bool UpdateEndHeder(BankHederModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE BankHeder SET  debcreEnd=@debcreEnd, dateEnd=@dateEnd, amountEnd=@amountEnd WHERE idBankHeder=@idBankHeder ");
            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idBankHeder", SqlDbType.Int);
            sqlParameter[0].Value = model.idBankHeder;
            sqlParameter[1] = new SqlParameter("@debcreEnd", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.debcreEnd;
            sqlParameter[2] = new SqlParameter("@dateEnd", SqlDbType.Date);
            sqlParameter[2].Value = model.dateEnd;
            sqlParameter[3] = new SqlParameter("@amountEnd", SqlDbType.Decimal);
            sqlParameter[3].Value = model.amountEnd;

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
            sqlParameter[4].Value = model.idBankHeder.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idBankHeder";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BankHeder";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save lines";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public bool Update(BankHederModel linemodel)
        {
            string query = string.Format(@"UPDATE BankHeder SET   entryDate=@entryDate,statementNo=@statementNo,accountNumber=@accountNumber, 
                                        debcrePrevius=@debcrePrevius, dateStatPrevius=@dateStatPrevius,amountPrevius=@amountPrevius,
                                        debcreEnd=@debcreEnd,dateEnd=@dateEnd,amountEnd=@amountEnd
                                        WHERE idBankHeder=@idBankHeder ");

            SqlParameter[] sqlParameters = new SqlParameter[10];

            sqlParameters[0] = new SqlParameter("@statementNo", SqlDbType.NVarChar);
            sqlParameters[0].Value = linemodel.statementNo;

            sqlParameters[1] = new SqlParameter("@accountNumber", SqlDbType.NVarChar);
            sqlParameters[1].Value = linemodel.accountNumber;

            sqlParameters[2] = new SqlParameter("@debcrePrevius", SqlDbType.NVarChar);
            sqlParameters[2].Value = linemodel.debcrePrevius;

            sqlParameters[3] = new SqlParameter("@entryDate", SqlDbType.Date);
            sqlParameters[3].Value = linemodel.entryDate;

            sqlParameters[4] = new SqlParameter("@dateStatPrevius", SqlDbType.Date);
            sqlParameters[4].Value = linemodel.dateStatPrevius;

            sqlParameters[5] = new SqlParameter("@amountPrevius", SqlDbType.Decimal);
            sqlParameters[5].Value = linemodel.amountPrevius;

            sqlParameters[6] = new SqlParameter("@debcreEnd", SqlDbType.NVarChar);
            sqlParameters[6].Value = linemodel.debcreEnd;

            sqlParameters[7] = new SqlParameter("@dateEnd", SqlDbType.Date);
            sqlParameters[7].Value = linemodel.dateEnd;

            sqlParameters[8] = new SqlParameter("@amountEnd", SqlDbType.Decimal);
            sqlParameters[8].Value = linemodel.amountEnd;

            sqlParameters[9] = new SqlParameter("@idBankHeder", SqlDbType.Int);
            sqlParameters[9].Value = linemodel.idBankHeder;


            return conn.executeUpdateQuery(query, sqlParameters);

        }

        public DataTable GetLinesByHeder(int idHeder)
        {
            string query = string.Format(@"SELECT *  FROM BankLines WHERE idBankHeder=@idHeder ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idHeder", SqlDbType.Int);
            sqlParameters[0].Value = idHeder;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable CheckHeder(string statement)
        {
            string query = string.Format(@"SELECT *  FROM BankHeder WHERE statementNo=@statement ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@statement", SqlDbType.NVarChar);
            sqlParameters[0].Value = statement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool SaveLines(BankLinesModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string query = string.Format(@"INSERT INTO  BankLines ( idBankHeder,valueDate,debcreLine, idCustomer, amountLine,transactType,
                                        accountNo,payerLine,refNo,desc1Line,desc2Line,desc3Line,desc4Line,desc5Line,desc6Line,desc7Line,desc8Line,desc9Line )
                                 Values ( @idBankHeder,@valueDate,@debcreLine, @idCustomer, @amountLine,@transactType,
                                        @accountNo,@payerLine,@refNo,@desc1Line,@desc2Line,@desc3Line,@desc4Line,@desc5Line,@desc6Line,@desc7Line,@desc8Line,@desc9Line )");


            SqlParameter[] sqlParameter = new SqlParameter[18];

            sqlParameter[0] = new SqlParameter("@idBankHeder", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.idBankHeder;

            sqlParameter[1] = new SqlParameter("@valueDate", SqlDbType.Date);
            sqlParameter[1].Value = linemodel.valueDate;

            sqlParameter[2] = new SqlParameter("@debcreLine", SqlDbType.NVarChar);
            sqlParameter[2].Value = linemodel.debcreLine;

            sqlParameter[3] = new SqlParameter("@idCustomer", SqlDbType.NVarChar);
            sqlParameter[3].Value = linemodel.idCustomer;

            sqlParameter[4] = new SqlParameter("@amountLine", SqlDbType.Decimal);
            sqlParameter[4].Value = linemodel.amountLine;

            sqlParameter[5] = new SqlParameter("@transactType", SqlDbType.NVarChar);
            sqlParameter[5].Value = linemodel.transactType;

            sqlParameter[6] = new SqlParameter("@accountNo", SqlDbType.NVarChar);
            sqlParameter[6].Value = linemodel.accountNo;

            sqlParameter[7] = new SqlParameter("@payerLine", SqlDbType.NVarChar);
            sqlParameter[7].Value = linemodel.payerLine;

            sqlParameter[8] = new SqlParameter("@refNo", SqlDbType.NVarChar);
            sqlParameter[8].Value = linemodel.refNo;

            sqlParameter[9] = new SqlParameter("@desc1Line", SqlDbType.NVarChar);
            sqlParameter[9].Value = linemodel.desc1Line;

            sqlParameter[10] = new SqlParameter("@desc2Line", SqlDbType.NVarChar);
            sqlParameter[10].Value = linemodel.desc2Line;

            sqlParameter[11] = new SqlParameter("@desc3Line", SqlDbType.NVarChar);
            sqlParameter[11].Value = linemodel.desc3Line;

            sqlParameter[12] = new SqlParameter("@desc4Line", SqlDbType.NVarChar);
            sqlParameter[12].Value = linemodel.desc4Line;

            sqlParameter[13] = new SqlParameter("@desc5Line", SqlDbType.NVarChar);
            sqlParameter[13].Value = linemodel.desc5Line;

            sqlParameter[14] = new SqlParameter("@desc6Line", SqlDbType.NVarChar);
            sqlParameter[14].Value = linemodel.desc6Line;

            sqlParameter[15] = new SqlParameter("@desc7Line", SqlDbType.NVarChar);
            sqlParameter[15].Value = linemodel.desc7Line;

            sqlParameter[16] = new SqlParameter("@desc8Line", SqlDbType.NVarChar);
            sqlParameter[16].Value = linemodel.desc8Line;

            sqlParameter[17] = new SqlParameter("@desc9Line", SqlDbType.NVarChar);
            sqlParameter[17].Value = linemodel.desc9Line;


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
            sqlParameter[4].Value = conn.GetLastTableID("BankLines") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idBankLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BankLines";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save lines";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

           
        }
        public bool UpdateLines(BankLinesModel linemodel)
        {

            string query = string.Format(@"UPDATE  BankLines SET idBankHeder=@idBankHeder,valueDate=@valueDate,debcreLine=@debcreLine, idCustomer=@idCustomer, amountLine=@amountLine,
                                          transactType=@transactType,accountNo=@accountNo,payerLine=@payerLine,refNo=@refNo,
            desc1Line=@desc1Line,desc2Line=@desc2Line,desc3Line=@desc3Line,desc4Line=@desc4Line,desc5Line=@desc5Line,
            desc6Line=@desc6Line,desc7Line=@desc7Line,desc8Line=@desc8Line,desc9Line=@desc9Line WHERE idBankLine=@idBankLine )");


            SqlParameter[] sqlParameters = new SqlParameter[19];

            sqlParameters[0] = new SqlParameter("@idBankHeder", SqlDbType.Int);
            sqlParameters[0].Value = linemodel.idBankHeder;

            sqlParameters[1] = new SqlParameter("@valueDate", SqlDbType.NVarChar);
            sqlParameters[1].Value = linemodel.valueDate;

            sqlParameters[2] = new SqlParameter("@debcreLine", SqlDbType.NVarChar);
            sqlParameters[2].Value = linemodel.debcreLine;

            sqlParameters[3] = new SqlParameter("@idCustomer", SqlDbType.Date);
            sqlParameters[3].Value = linemodel.idCustomer;

            sqlParameters[4] = new SqlParameter("@amountLine", SqlDbType.Date);
            sqlParameters[4].Value = linemodel.amountLine;

            sqlParameters[5] = new SqlParameter("@transactType", SqlDbType.Decimal);
            sqlParameters[5].Value = linemodel.transactType;

            sqlParameters[6] = new SqlParameter("@accountNo", SqlDbType.NVarChar);
            sqlParameters[6].Value = linemodel.accountNo;

            sqlParameters[7] = new SqlParameter("@payerLine", SqlDbType.Date);
            sqlParameters[7].Value = linemodel.payerLine;

            sqlParameters[8] = new SqlParameter("@refNo", SqlDbType.Decimal);
            sqlParameters[8].Value = linemodel.refNo;

            sqlParameters[9] = new SqlParameter("@desc1Line", SqlDbType.NVarChar);
            sqlParameters[9].Value = linemodel.desc1Line;

            sqlParameters[10] = new SqlParameter("@desc2Line", SqlDbType.NVarChar);
            sqlParameters[10].Value = linemodel.desc2Line;

            sqlParameters[11] = new SqlParameter("@desc3Line", SqlDbType.NVarChar);
            sqlParameters[11].Value = linemodel.desc3Line;

            sqlParameters[12] = new SqlParameter("@desc4Line", SqlDbType.NVarChar);
            sqlParameters[12].Value = linemodel.desc4Line;

            sqlParameters[13] = new SqlParameter("@desc5Line", SqlDbType.NVarChar);
            sqlParameters[13].Value = linemodel.desc5Line;

            sqlParameters[14] = new SqlParameter("@desc6Line", SqlDbType.NVarChar);
            sqlParameters[14].Value = linemodel.desc6Line;

            sqlParameters[15] = new SqlParameter("@desc7Line", SqlDbType.NVarChar);
            sqlParameters[15].Value = linemodel.desc7Line;

            sqlParameters[16] = new SqlParameter("@desc8Line", SqlDbType.NVarChar);
            sqlParameters[16].Value = linemodel.desc8Line;

            sqlParameters[17] = new SqlParameter("@desc9Line", SqlDbType.NVarChar);
            sqlParameters[17].Value = linemodel.desc9Line;

            sqlParameters[18] = new SqlParameter("@idBankLine", SqlDbType.Int);
            sqlParameters[18].Value = linemodel.idBankLine;

            return conn.executeUpdateQuery(query, sqlParameters);
        }
    }
}
