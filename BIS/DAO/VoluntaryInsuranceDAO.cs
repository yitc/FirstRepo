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
    public class VoluntaryInsuranceDAO
    {
        private dbConnection conn;

        public VoluntaryInsuranceDAO()
        {
            conn = new dbConnection();
        }
        public DataTable GetAllVoluntaryInsurance(DateTime dtFrom, DateTime dtTo, string userName)
        {
            string query = string.Format(
                @"select a.dtFromArrangement,a.codeArrangement,c.premie as code,vfq.txtQuest , ab.idContPers, cpf.idFilter, CASE WHEN t.nameTitle is NULL THEN '' ELSE t.nameTitle END +' '+ CASE WHEN cp.initialsContPers is NULL THEN '' ELSE cp.initialsContPers END + ' '+
                     CASE WHEN cp.midname is NULL THEN '' ELSE cp.midname END + ' ' + CASE WHEN cp.lastname is NULL THEN '' ELSE cp.lastname END as name, DATEDIFF(DAY, a.dtFromArrangement  ,a.dtToArrangement )+1 as daysTrip
                     ,aip.amountPremie
                    from ArrangementBook as ab
                    left outer join ContactPersonFilter as cpf on ab.idContPers=cpf.idContPers
                    left outer join Arrangement as a on ab.idArrangement=a.idArrangement
                    left outer join ContactPerson as cp on ab.idContPers=cp.idContPers
                    left outer join Title as t on t.idTitle=cp.idTitle
                    left outer join Country as c on a.countryArrangement = c.idCountry
                    inner join VolLookup as vl on ab.idContPers = vl.idContPers and ab.idArrangement = vl.idArrangement and vl.type = 'F' and (vl.id = 1 or vl.id = 2 or vl.id = 3 or vl.id = 4 or vl.id = 5)
                    left outer join VolFunctionQuest as vfq on vl.id = vfq.idQuest
                    left outer join ArrangementInsurancePremie as aip on c.premie = aip.codeInsurance and aip.premie = CASE WHEN  (vl.id = 1 or vl.id = 2) THEN 'Premie 1' ELSE (CASE WHEN (vl.id = 3 or vl.id = 4 or vl.id = 5) THEN 'Premie 2' ELSE '' END) END and  a.dtFromArrangement >= aip.dtValidFrom and a.dtToArrangement <= aip.dtValidTo
                    where idFilter=4 and c.premie is not null and aip.amountPremie is not null and a.dtFromArrangement>=@dtFrom and a.dtToArrangement<=@dtTo
                  ");


            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;

            DataTable dt = conn.executeSelectQuery(query, sqlParameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("Username", typeof(string));
                dt.Columns.Add("From", typeof(DateTime));
                dt.Columns.Add("To", typeof(DateTime));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["From"] = dtFrom;
                    dt.Rows[i]["To"] = dtTo;
                    dt.Rows[i]["Username"] = userName;

                }
            }
            return dt;

        }


    }
}
