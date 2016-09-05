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
    public class TypesAddressBUS
    {
        TypesAddressDAO tDAO;

        public TypesAddressBUS()
        {
            tDAO = new TypesAddressDAO();
        }

        public List<TypesAddresslModel> GetAllTypeAdress(string idLang)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = tDAO.GetAllTypeAddress(idLang);

                List<TypesAddresslModel> typesaddress = new List<TypesAddresslModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TypesAddresslModel model = new TypesAddresslModel();

                            model.idAddressType = Int32.Parse(dr["idAddressType"].ToString());
                            model.nameAddressType = dr["nameAddressType"].ToString();

                            if (dr["showInControl"].ToString() != "")
                                model.showInControl = Boolean.Parse(dr["showInControl"].ToString());


                            typesaddress.Add(model);
                        }
                        return typesaddress;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TypesAddresslModel GetTypeAddressById(int idType, string idLang)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = tDAO.GetTypeAddressById(idType, idLang);

                TypesAddresslModel model = new TypesAddresslModel();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            

                            model.idAddressType = Int32.Parse(dr["idAddressType"].ToString());
                            model.nameAddressType = dr["nameAddressType"].ToString();

                            if (dr["showInControl"].ToString() != "")
                                model.showInControl = Boolean.Parse(dr["showInControl"].ToString());                            
                        }
                        return model;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
