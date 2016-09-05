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
    public class PrintReportDAO
    {
        private dbConnection conn;
        public PrintReportDAO()
        {
            conn = new dbConnection();

        }
        public DataTable GetReportsForPrintingTraveler(int idArrangement)
        {
             //and statusLine='" + openclose.ToString() + "'  
            string query = string.Format(@" select DISTINCT atp.idArrangement, ab.idContPers, nameTravelPapers
                                            from ArrangementTravelPapers as atp
                                            left outer join ArrangementBook as ab on atp.idArrangement = ab.idArrangement
                                            left outer join TravelPapers2 as tp2 on tp2.idTravelPapers = atp.idTravelPapers
                                            left outer join ContactPersonFilter as cpf on cpf.idContPers = ab.idContPers
                                            where cpf.idFilter<>4 and  tp2.idFilter<>4 and atp.idArrangement="+idArrangement+" and ab.idStatus = '2'");
             return conn.executeSelectQuery(query, null);
        }
        public DataTable GetReportsForPrintingVolunteers(int idArrangement)
        {
            //and statusLine='" + openclose.ToString() + "'  
            string query = string.Format(@" select atp.idArrangement, ab.idContPers, nameTravelPapers
                                            from ArrangementTravelPapers as atp
                                            left outer join ArrangementBook as ab on atp.idArrangement = ab.idArrangement
                                            left outer join TravelPapers2 as tp2 on tp2.idTravelPapers = atp.idTravelPapers
                                            left outer join ContactPersonFilter as cpf on cpf.idContPers = ab.idContPers
                                            where cpf.idFilter=4 and  tp2.idFilter=4 and atp.idArrangement="+idArrangement+" and ab.idStatus = '2'");
            return conn.executeSelectQuery(query, null);
        }




    }
}
