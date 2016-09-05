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
    public class InvoiceItemsDAO
    {
        private dbConnection conn;
        public InvoiceItemsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetInvoiceItemsByInvoice(string invoice, string lang)
        {
            string query = string.Format(@"SELECT idInvItem, ii.idInvoice,  idArtical ,CASE WHEN idArtical ='Reis Pakket' THEN ara.nameArrangement ELSE CASE WHEN idArtical ='Insurance' THEN aip.descriptionArticle ELSE CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE CASE WHEN c.nameArtical IS NULL OR c.nameArtical='' THEN ii.idArtical ELSE c.nameArtical END  END END END as nameArtical, ii.price, ii.quantity , (ii.price * ii.quantity) as itemSum, ii.userCreated, ii.dtCreated, ii.userModified, ii.dtModified,
                                           isSecondGrid,isCancelationIns,ii.isMedical
                                           FROM InvoiceItems ii
                                           LEFT  JOIN Artical c on c.codeArtical = ii.idArtical 
                                           LEFT OUTER JOIN STRING" + lang + @" s ON s.stringKey = ii.idArtical
                                           LEFT OUTER JOIN Invoice i ON ii.idInvoice = i.idInvoice
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = ab.idArrangement
                                           LEFT OUTER JOIN (SELECT ati.description as descriptionArticle,aip.idArrangement,idArticle,ati.isMedicalDevices FROM ArrangementInvoicePrice aip 
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = aip.idArrangement 
                                           LEFT OUTER JOIN ArrangementCalculation calc ON ara.idArrangement = calc.idArrangement
                                           LEFT OUTER JOIN Country co ON ara.countryArrangement = co.idCountry 
                                           LEFT OUTER JOIN ArrangementTravelInsurance ati ON ati.codeInsurance = co.premie AND calc.isSport = ati.isSportActivity
                                           WHERE idArticle = 'Insurance') aip ON aip.idArrangement = ara.idArrangement AND  aip.idArticle = ii.idArtical AND ii.isMedical = aip.isMedicalDevices
                                            WHERE ii.idInvoice = '" + invoice.ToString() + "' and isSecondGrid=0 ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetInvoiceItemsByInvoiceSecond(string invoice, string lang)
        {
            string query = string.Format(@"SELECT idInvItem, ii.idInvoice, idArtical, CASE WHEN idArtical ='Reis Pakket' THEN ara.nameArrangement ELSE CASE WHEN idArtical ='Insurance' THEN aip.descriptionArticle ELSE CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE CASE WHEN c.nameArtical IS NULL OR c.nameArtical='' THEN ii.idArtical ELSE c.nameArtical END  END END END as nameArtical, ii.price, ii.quantity , ii.userCreated, ii.dtCreated, ii.userModified, ii.dtModified,
                                            isSecondGrid,isCancelationIns,ii.isMedical
                                            FROM InvoiceItems ii
                                            LEFT  JOIN Artical c on c.codeArtical = ii.idArtical 
                                           LEFT OUTER JOIN STRING" + lang + @" s ON s.stringKey = ii.idArtical
                                           LEFT OUTER JOIN Invoice i ON ii.idInvoice = i.idInvoice
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = ab.idArrangement
                                           LEFT OUTER JOIN (SELECT ati.description as descriptionArticle,aip.idArrangement,idArticle,ati.isMedicalDevices FROM ArrangementInvoicePrice aip 
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = aip.idArrangement 
                                           LEFT OUTER JOIN ArrangementCalculation calc ON ara.idArrangement = calc.idArrangement
                                           LEFT OUTER JOIN Country co ON ara.countryArrangement = co.idCountry 
                                           LEFT OUTER JOIN ArrangementTravelInsurance ati ON ati.codeInsurance = co.premie AND calc.isSport = ati.isSportActivity
                                           WHERE idArticle = 'Insurance') aip ON aip.idArrangement = ara.idArrangement AND  aip.idArticle = ii.idArtical AND ii.isMedical = aip.isMedicalDevices
                                            WHERE idInvoice = '" + invoice.ToString() + "' and isSecondGrid=1 ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllInvoiceItems( string lang)
        {
            string query = string.Format(@"SELECT idInvItem, ii.idInvoice, idArtical,  CASE WHEN idArtical ='Reis Pakket' THEN ara.nameArrangement ELSE CASE WHEN idArtical ='Insurance' THEN aip.descriptionArticle ELSE CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE CASE WHEN c.nameArtical IS NULL OR c.nameArtical='' THEN ii.idArtical ELSE c.nameArtical END  END END END as nameArtical, ii.price, ii.quantity , ii.userCreated, ii.dtCreated, ii.userModified, ii.dtModified,
                                             isSecondGrid,isCancelationIns,ii.isMedical
                                              FROM InvoiceItems ii
                                             LEFT  JOIN Artical c on c.codeArtical = ii.idArtical 
                                             LEFT OUTER JOIN STRING" + lang + @" s ON s.stringKey = ii.idArtical
                                            LEFT OUTER JOIN Invoice i ON ii.idInvoice = i.idInvoice
                                             LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                             LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = ab.idArrangement
                                            LEFT OUTER JOIN (SELECT ati.description as descriptionArticle,aip.idArrangement,idArticle,ati.isMedicalDevices FROM ArrangementInvoicePrice aip 
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = aip.idArrangement 
                                           LEFT OUTER JOIN ArrangementCalculation calc ON ara.idArrangement = calc.idArrangement
                                           LEFT OUTER JOIN Country co ON ara.countryArrangement = co.idCountry 
                                           LEFT OUTER JOIN ArrangementTravelInsurance ati ON ati.codeInsurance = co.premie AND calc.isSport = ati.isSportActivity
                                           WHERE idArticle = 'Insurance') aip ON aip.idArrangement = ara.idArrangement AND  aip.idArticle = ii.idArtical AND ii.isMedical = aip.isMedicalDevices
                                            ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetInvoiceItemsByID(int idInvoice, string lang)
        {
//            string query = string.Format(@"SELECT idInvItem, idInvoice, idArtical,c.nameArtical price, quantity,userCreated, dtCreated, userModified, dtModified 
//                                           FROM InvoiceItems
//                                           LEFT INNER JOIN Artical c on c.idArtical = idArtical 
//                                            WHERE idInvoice=@idInvoice");
            string query = string.Format(@"SELECT idInvItem, a.idInvoice, a.idArtical,  CASE WHEN idArtical ='Reis Pakket' THEN ara.nameArrangement ELSE CASE WHEN idArtical ='Insurance' THEN aip.descriptionArticle ELSE CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE CASE WHEN c.nameArtical IS NULL OR c.nameArtical='' THEN a.idArtical ELSE c.nameArtical END  END END END as nameArtical, a.price, a.quantity,(a.price*a.quantity) as itemSum, a.userCreated, a.dtCreated, a.userModified, a.dtModified,
                                           isSecondGrid,isCancelationIns,a.isMedical
                                           FROM InvoiceItems a
                                           LEFT  JOIN Artical c on c.codeArtical = a.idArtical 
                                           LEFT OUTER JOIN STRING" + lang + @" s ON s.stringKey = a.idArtical
                                           LEFT OUTER JOIN Invoice i ON a.idInvoice = i.idInvoice
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = ab.idArrangement
                                          LEFT OUTER JOIN (SELECT ati.description as descriptionArticle,aip.idArrangement,idArticle,ati.isMedicalDevices FROM ArrangementInvoicePrice aip 
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = aip.idArrangement 
                                           LEFT OUTER JOIN ArrangementCalculation calc ON ara.idArrangement = calc.idArrangement
                                           LEFT OUTER JOIN Country co ON ara.countryArrangement = co.idCountry 
                                           LEFT OUTER JOIN ArrangementTravelInsurance ati ON ati.codeInsurance = co.premie AND calc.isSport = ati.isSportActivity
                                           WHERE idArticle = 'Insurance') aip ON aip.idArrangement = ara.idArrangement AND  aip.idArticle = a.idArtical AND a.isMedical = aip.isMedicalDevices
                                            WHERE a.idInvoice=@idInvoice ");
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetInvoiceItemsByIDNoFirst(int idInvoice, string lang)
        {
            //            string query = string.Format(@"SELECT idInvItem, idInvoice, idArtical,c.nameArtical price, quantity,userCreated, dtCreated, userModified, dtModified 
            //                                           FROM InvoiceItems
            //                                           LEFT INNER JOIN Artical c on c.idArtical = idArtical 
            //                                            WHERE idInvoice=@idInvoice");
            string query = string.Format(@"SELECT idInvItem, a.idInvoice, a.idArtical, CASE WHEN idArtical ='Reis Pakket' THEN ara.nameArrangement ELSE CASE WHEN idArtical ='Insurance' THEN aip.descriptionArticle ELSE CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE CASE WHEN c.nameArtical IS NULL OR c.nameArtical='' THEN a.idArtical ELSE c.nameArtical END  END END END as nameArtical,
                                           a.price, a.quantity,a.userCreated, a.dtCreated, a.userModified, a.dtModified,
                                           isSecondGrid,isCancelationIns,a.isMedical
                                           FROM InvoiceItems a
                                           LEFT  JOIN Artical c on c.codeArtical = a.idArtical 
                                           LEFT OUTER JOIN STRING" + lang + @" s ON s.stringKey = a.idArtical
                                           LEFT OUTER JOIN Invoice i ON a.idInvoice = i.idInvoice
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = ab.idArrangement
                                          LEFT OUTER JOIN (SELECT ati.description as descriptionArticle,aip.idArrangement,idArticle,ati.isMedicalDevices FROM ArrangementInvoicePrice aip 
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = aip.idArrangement 
                                           LEFT OUTER JOIN ArrangementCalculation calc ON ara.idArrangement = calc.idArrangement
                                           LEFT OUTER JOIN Country co ON ara.countryArrangement = co.idCountry 
                                           LEFT OUTER JOIN ArrangementTravelInsurance ati ON ati.codeInsurance = co.premie AND calc.isSport = ati.isSportActivity
                                           WHERE idArticle = 'Insurance') aip ON aip.idArrangement = ara.idArrangement AND  aip.idArticle = a.idArtical AND a.isMedical = aip.isMedicalDevices
                                            WHERE a.idInvoice=@idInvoice and a.idArtical != 'First payment' ");


            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable GetInvoiceItemsByIDWithFirst(int idInvoice, string lang)
        {
            //            string query = string.Format(@"SELECT idInvItem, idInvoice, idArtical,c.nameArtical price, quantity,userCreated, dtCreated, userModified, dtModified 
            //                                           FROM InvoiceItems
            //                                           LEFT INNER JOIN Artical c on c.idArtical = idArtical 
            //                                            WHERE idInvoice=@idInvoice");
            string query = string.Format(@" SELECT idInvItem,a. idInvoice, a.idArtical, CASE WHEN idArtical ='Reis Pakket' THEN ara.nameArrangement ELSE CASE WHEN idArtical ='Insurance' THEN aip.descriptionArticle ELSE CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE CASE WHEN c.nameArtical IS NULL OR c.nameArtical='' THEN a.idArtical ELSE c.nameArtical END  END END END as nameArtical
                                            ,a.price, a.quantity,a.userCreated, a.dtCreated, a.userModified, a.dtModified,
                                            isSecondGrid,isCancelationIns,a.isMedical
                                           FROM InvoiceItems a
                                           LEFT  JOIN Artical c on c.codeArtical = a.idArtical 
                                           LEFT OUTER JOIN STRING" + lang + @" s ON s.stringKey = a.idArtical
                                           LEFT OUTER JOIN Invoice i ON a.idInvoice = i.idInvoice
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = ab.idArrangement
                                          LEFT OUTER JOIN (SELECT ati.description as descriptionArticle,aip.idArrangement,idArticle,ati.isMedicalDevices FROM ArrangementInvoicePrice aip 
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = aip.idArrangement 
                                           LEFT OUTER JOIN ArrangementCalculation calc ON ara.idArrangement = calc.idArrangement
                                           LEFT OUTER JOIN Country co ON ara.countryArrangement = co.idCountry 
                                           LEFT OUTER JOIN ArrangementTravelInsurance ati ON ati.codeInsurance = co.premie AND calc.isSport = ati.isSportActivity
                                           WHERE idArticle = 'Insurance') aip ON aip.idArrangement = ara.idArrangement AND  aip.idArticle = a.idArtical AND a.isMedical = aip.isMedicalDevices
                                            WHERE a.idInvoice=@idInvoice ");
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetInvoiceItemsByInvoiceNrWithPrice(int invoiceNr, string lang)
        {
            string query = string.Format(@" SELECT idInvItem, a.idInvoice, a.idArtical, CASE WHEN idArtical ='Reis Pakket' THEN ara.nameArrangement ELSE CASE WHEN idArtical ='Insurance' THEN aip.descriptionArticle ELSE CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE CASE WHEN c.nameArtical IS NULL OR c.nameArtical='' THEN a.idArtical ELSE c.nameArtical END  END END END as nameArtical
                                           ,a.price, a.quantity,a.userCreated, a.dtCreated, a.userModified, a.dtModified,
                                           isSecondGrid,isCancelationIns,a.isMedical
                                           FROM InvoiceItems a
                                           LEFT  JOIN Artical c on c.codeArtical = a.idArtical 
                                           LEFT OUTER JOIN STRING" + lang + @" s ON s.stringKey = a.idArtical
                                           LEFT OUTER JOIN Invoice i ON a.idInvoice = i.idInvoice
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = ab.idArrangement
                                           LEFT OUTER JOIN (SELECT ati.description as descriptionArticle,aip.idArrangement,idArticle,ati.isMedicalDevices FROM ArrangementInvoicePrice aip 
                                           LEFT OUTER JOIN Arrangement ara ON ara.idArrangement = aip.idArrangement 
                                           LEFT OUTER JOIN ArrangementCalculation calc ON ara.idArrangement = calc.idArrangement
                                           LEFT OUTER JOIN Country co ON ara.countryArrangement = co.idCountry 
                                           LEFT OUTER JOIN ArrangementTravelInsurance ati ON ati.codeInsurance = co.premie AND calc.isSport = ati.isSportActivity
                                           WHERE idArticle = 'Insurance') aip ON aip.idArrangement = ara.idArrangement AND  aip.idArticle = a.idArtical AND a.isMedical = aip.isMedicalDevices
                                           WHERE a.idInvoice IN (SELECT idInvoice FROM Invoice WHERE invoiceNr = '" + invoiceNr + "' AND invoiceRbr = '001' AND brutoAmount>0) ");
            

            return conn.executeSelectQuery(query, null);
        }

        public Boolean Save(InvoiceItemsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO InvoiceItems (idInvoice, idArtical, price, quantity, userCreated, dtCreated, userModified, dtModified,
                                           isSecondGrid,isCancelationIns,isMedical )
                                           VALUES (@idInvoice, @idArtical, @price, @quantity, @userCreated, @dtCreated, @userModified, @dtModified,
                                           @isSecondGrid,@isCancelationIns,@isMedical)");

            SqlParameter[] sqlParameter = new SqlParameter[11];

            sqlParameter[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameter[0].Value = model.idInvoice;

            sqlParameter[1] = new SqlParameter("@idArtical", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.idArtical;

            sqlParameter[2] = new SqlParameter("@price", SqlDbType.Decimal);
            sqlParameter[2].Value = model.price;

            sqlParameter[3] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameter[3].Value = model.quantity;

            sqlParameter[4] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[4].Value = model.userCreated;

            sqlParameter[5] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtCreated;

            sqlParameter[6] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[6].Value = model.userModified;

            sqlParameter[7] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtModified;

            sqlParameter[8] = new SqlParameter("@isSecondGrid", SqlDbType.Bit);
            sqlParameter[8].Value = model.isSecondGrid;

            sqlParameter[9] = new SqlParameter("@isCancelationIns", SqlDbType.Bit);
            sqlParameter[9].Value = model.isCancelationIns;

            sqlParameter[10] = new SqlParameter("@isMedical", SqlDbType.Bit);
            sqlParameter[10].Value = model.isMedical;

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
            sqlParameter[4].Value = conn.GetLastTableID("InvoiceItems")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInvItem";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "InvoiceItems";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean SaveItemsTransaction(List<InvoiceItemsModel> modelList, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            SqlParameter[] sqlParameter;
            string query;
            int idfrom = conn.GetLastTableID("InvoiceItems") + 1;
            foreach (InvoiceItemsModel model in modelList)
            {
                query = string.Format(@"INSERT INTO InvoiceItems (idInvoice, idArtical, price, quantity, userCreated, dtCreated, userModified, dtModified,
                                           isSecondGrid,isCancelationIns,isMedical )
                                           VALUES (@idInvoice, @idArtical, @price, @quantity, @userCreated, @dtCreated, @userModified, @dtModified,
                                           @isSecondGrid,@isCancelationIns,@isMedical)");


                sqlParameter = new SqlParameter[11];
                sqlParameter[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
                sqlParameter[0].Value = model.idInvoice;

                sqlParameter[1] = new SqlParameter("@idArtical", SqlDbType.NVarChar);
                sqlParameter[1].Value = model.idArtical;

                sqlParameter[2] = new SqlParameter("@price", SqlDbType.Decimal);
                sqlParameter[2].Value = model.price;

                sqlParameter[3] = new SqlParameter("@quantity", SqlDbType.Int);
                sqlParameter[3].Value = model.quantity;

                sqlParameter[4] = new SqlParameter("@userCreated", SqlDbType.Int);
                sqlParameter[4].Value = model.userCreated;

                sqlParameter[5] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
                sqlParameter[5].Value = model.dtCreated;

                sqlParameter[6] = new SqlParameter("@userModified", SqlDbType.Int);
                sqlParameter[6].Value = model.userModified;

                sqlParameter[7] = new SqlParameter("@dtModified", SqlDbType.DateTime);
                sqlParameter[7].Value = model.dtModified;

                sqlParameter[8] = new SqlParameter("@isSecondGrid", SqlDbType.Bit);
                sqlParameter[8].Value = model.isSecondGrid;

                sqlParameter[9] = new SqlParameter("@isCancelationIns", SqlDbType.Bit);
                sqlParameter[9].Value = model.isCancelationIns;

                sqlParameter[10] = new SqlParameter("@isMedical", SqlDbType.Bit);
                sqlParameter[10].Value = model.isMedical;

                _query.Add(query);
                sqlParameters.Add(sqlParameter);

               
            }
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
            sqlParameter[4].Value = idfrom +"-"+ (conn.GetLastTableID("InvoiceItems")+1).ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInvItem";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "InvoiceItems";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save items transaction";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);
            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int invoice, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE InvoiceItems WHERE idInvoice = @invoice");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@invoice", SqlDbType.Int);
            sqlParameter[0].Value = invoice;

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
            sqlParameter[4].Value = invoice;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "Invoice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "InvoiceItems";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}