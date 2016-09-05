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
    public class ContactPersonTripDataDAO
    {
        private dbConnection conn;

        public ContactPersonTripDataDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetTripByPerson(int idContactPerson)
        {
            string query = string.Format(@"SELECT a.idContPersTravel,a.helpP, a.idContactPerson, a.dtFrom,a.dtTo,a.descriptionTripSort, a.idTargetGroup, ag.shortcutTargeGroup,ag.nameTargetGroup, a.op1, a.op2,a.op3
                                        FROM ContactPersonTripData a 
                                        LEFT OUTER JOIN TargetGroup ag ON a.idTargetGroup = ag.idTargetGroup  
                                        WHERE a.idContactPerson= '" + idContactPerson.ToString() + "'");

            return conn.executeSelectQuery(query, null);
        }


        public Boolean Save(ContactPersonTripDataModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ContactPersonTripData (idContactPerson, dtFrom, dtTo,descriptionTripSort, idTargetGroup, op1, op2, op3,helpP )
                      VALUES (@idContactPerson, @dtFrom, @dtTo,@descriptionTripSort, @idTargetGroup,@op1, @op2, @op3, @helpP )");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@idContactPerson", SqlDbType.Int);
            sqlParameter[0].Value = model.idContactPerson;

            sqlParameter[1] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameter[1].Value = model.dtFrom;

            sqlParameter[2] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameter[2].Value = model.dtTo;

            sqlParameter[3] = new SqlParameter("@idTargetGroup", SqlDbType.Int);
            sqlParameter[3].Value = model.idTargetGroup;

            sqlParameter[4] = new SqlParameter("@op1", SqlDbType.Bit);
            sqlParameter[4].Value = model.op1;

            sqlParameter[5] = new SqlParameter("@op2", SqlDbType.Bit);
            sqlParameter[5].Value = model.op2;

            sqlParameter[6] = new SqlParameter("@op3", SqlDbType.Bit);
            sqlParameter[6].Value = model.op3;

            sqlParameter[7] = new SqlParameter("@descriptionTripSort", SqlDbType.NVarChar);
            sqlParameter[7].Value = model.descriptionTripSort;

            sqlParameter[8] = new SqlParameter("@helpP", SqlDbType.NVarChar);
            sqlParameter[8].Value = model.helpP;



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
            sqlParameter[4].Value = conn.GetLastTableID("ContactPersonTripData");

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContPersTravel";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonTripData";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
     
        public Boolean Delete(int id, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ContactPersonTripData 
                    WHERE idContPersTravel = @id");


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
            sqlParameter[4].Value = conn.GetLastTableID("ContactPersonTripData");

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContPersTravel";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonTripData";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
