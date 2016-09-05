using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.DAO;
using BIS.Model;
using System.Data;

namespace BIS.Business
{
    public class OverviewBookingBUS
    {
        private ArrangementBookDAO ABdao;

        public OverviewBookingBUS()
        {
            ABdao = new ArrangementBookDAO();
        }
        public List<OverviewBookingFormModel> getOverviewBooking(DateTime dtfrom, DateTime dtTo, int status, int idArrangement, List<int> label, string travelPapers)
        {
            DataTable dataTable = new DataTable();
            dataTable = ABdao.GetOverviewBooking(dtfrom, dtTo, status, idArrangement, label, travelPapers);
            OverviewBookingFormModel model = new OverviewBookingFormModel();
            List<OverviewBookingFormModel> list = new List<OverviewBookingFormModel>();
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    //  AccSepaModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new OverviewBookingFormModel();
                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["fullName"].ToString() != "")
                            model.personFullName = dr["fullName"].ToString();
                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();
                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());
                        //if (dr["amount"].ToString() != "")
                        //    model.amount = dr["amount"].ToString()+"€";
                        if (dr["dtUserCreated"].ToString() != "")
                            model.dateCreated = DateTime.Parse(dr["dtUserCreated"].ToString());
                        if (dr["nameUser"].ToString() != "")
                            model.userCreated = dr["nameUser"].ToString();
                        if (dr["status"].ToString() != "")
                            model.status = dr["status"].ToString();

                        if (dr["nameTravelPapers"].ToString() != null)
                            model.nameTravelPapers = dr["nameTravelPapers"].ToString();
                        list.Add(model);
                    }
                    return list;

                }
                else
                    return null;
            }
            else
                return null;
        }

        }
    }
