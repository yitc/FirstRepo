using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;


namespace BIS.DAO
{
    public class DocumentTypeDAO
    {
        private dbConnection conn;

        public DocumentTypeDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllDocumentTypes()
        {
            string query = string.Format(@"SELECT idDocumentType, typeDocument, nameDocumentType, extendDocumentType,
                                        haveLayout,tableDocumentType,dtCreted,dtModified,defaultBookmark,idModifiedUser,idCreatedUser  , uc.nameUser as nameUserCreated, um.nameUser as nameUserModified
                                        FROM DocumentType dt
                                        LEFT OUTER JOIN Users uc ON uc.idUser = dt.idCreatedUser
                                        LEFT OUTER JOIN Users um ON um.idUser = dt.idModifiedUser
                                        ORDER BY idDocumentType");

            return conn.executeSelectQuery(query, null);
        }

    }
}