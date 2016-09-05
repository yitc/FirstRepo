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
    public class ExtraArticleDAO
    {
        private dbConnection conn;

        public ExtraArticleDAO()
        {
            conn = new dbConnection();
        }
        public DataTable GetAllExtraArticle(DateTime dtFrom, DateTime dtTo, List<LabelForArrangement> ArrangementLabel, String name)
        {
            string query1 = "";
            int labels = ArrangementLabel.Count();
            if (ArrangementLabel.Count > 0)
                    {
                        query1 += "(";
                        int count = 0;
                        foreach (var ar in ArrangementLabel)
                    {


                    if (count == 0)
                        query1 += "al.idLabel = '" + ar.idLabel.ToString() + "' ";

                    if (count > 0)
                        query1 += " OR al.idLabel = '" + ar.idLabel.ToString() + "' ";


                    count++;
                    }
                query1 += ")";
            }
                
              
                string query = string.Format(
                @"select a.nameArrangement,art.nameArtical , a.dtFromArrangement, ii.invoiceNr+'-'+ii.invoiceRbr as invoicefullNumber,case when cp.firstname is null then '' else cp.firstname end +' '+case when cp.lastname is null then '' else cp.lastname end fullName, ii.price
                from ArrangementInvoicePrice as aip
                left join Arrangement as a on a.idArrangement = aip.idArrangement
                left join Artical as art on art.codeArtical = aip.idArticle
                left outer join ArrangementBook as ab on aip.idArrangement= ab.idArrangement
                left outer join ArrangementBookArticles as aba on aba.idArrangementBook = ab.idArrangementBook and aba.idArticle = aip.idArticle
                inner join (SELECT idVoucher, idArtical,invoiceNr,invoiceRbr,price FROM Invoice i
                LEFT OUTER JOIN InvoiceItems ii ON ii.idInvoice = i.idInvoice) ii on ii.idVoucher = aba.idArrangementBook and ii.idArtical = aip.idArticle 
                left join ContactPerson as cp on cp.idContPers = ab.idContPers
                left join ArrangementLabel as al on al.idArrangement = a.idArrangement
                where aip.isExtra = 1 and invoiceNr IS NOT NULL and invoiceRbr is not null and " + query1 + " and a.dtFromArrangement>=@dtFrom and a.dtToArrangement<=@dtTo");
                
            
                SqlParameter[] sqlParameters = new SqlParameter[2];
                sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
                sqlParameters[0].Value = dtFrom;
                sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
                sqlParameters[1].Value = dtTo;

                DataTable dt = conn.executeSelectQuery(query, sqlParameters);
                if (dt != null && dt.Rows.Count>0)
                {   
                    dt.Columns.Add("DateFrom", typeof(DateTime));
                    dt.Columns.Add("DateTo", typeof(DateTime));
                    dt.Columns.Add("NameLogin", typeof(string));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dt.Rows[i]["DateFrom"] = dtFrom;
                        dt.Rows[i]["DateTo"] = dtTo;
                        dt.Rows[i]["NameLogin"] = name;
                    }
                }
                
                return dt;
        }


    }
}
