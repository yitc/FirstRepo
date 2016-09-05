using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;

namespace BIS.Business
{
    public class ContactPersonTripDataBUS
    {
        private ContactPersonTripDataDAO contactPersonTripDataDAO;

        public ContactPersonTripDataBUS()
        {
            contactPersonTripDataDAO = new ContactPersonTripDataDAO();
        }

        public bool Save(ContactPersonTripDataModel contactPersonTripData, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = contactPersonTripDataDAO.Save(contactPersonTripData, nameForm , idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Delete(int id, string nameForm, int idUsers)
        {
            bool retval = false;
            try
            {
                retval = contactPersonTripDataDAO.Delete(id, nameForm, idUsers );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<ContactPersonTripDataModel> GetTripByPerson(int idContactPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactPersonTripDataDAO.GetTripByPerson(idContactPerson);
            List<ContactPersonTripDataModel> contactPersonTripData = new List<ContactPersonTripDataModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ContactPersonTripDataModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ContactPersonTripDataModel();

                        if (dr["idContPersTravel"].ToString() != "")
                        {
                            model.idContPersTravel = Int32.Parse(dr["idContPersTravel"].ToString());
                        }

                        if (dr["idContactPerson"].ToString() != "")
                        {
                            model.idContactPerson = Int32.Parse(dr["idContactPerson"].ToString());
                        }

                        if (dr["dtFrom"].ToString() != "")
                        {
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());
                        }

                        if (dr["dtTo"].ToString() != "")
                        {
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());
                        }

                        model.descriptionTripSort = dr["descriptionTripSort"].ToString(); //

                        if (dr["idTargetGroup"].ToString() != "")
                        {
                            model.idTargetGroup = Int32.Parse(dr["idTargetGroup"].ToString());
                        }

                        if (dr["op1"].ToString() != "")
                        {
                            model.op1 = Boolean.Parse(dr["op1"].ToString());
                        }

                        if (dr["op2"].ToString() != "")
                        {
                            model.op2 = Boolean.Parse(dr["op2"].ToString());
                        }

                        if (dr["op3"].ToString() != "")
                        {
                            model.op3 = Boolean.Parse(dr["op3"].ToString());
                        }

                        model.nameTargetGroup = dr["nameTargetGroup"].ToString();
                        model.shortcutTargeGroup = dr["shortcutTargeGroup"].ToString();
                        model.helpP = dr["helpP"].ToString();

                        contactPersonTripData.Add(model);
                    }
                    return contactPersonTripData;
                }
                else
                    return null;
            }
            else
                return null;

        }
    }
}