using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;
using System.ComponentModel;

namespace BIS.DAO
{
    public class ArrangementRoomsDAO
    {
        private dbConnection conn;

        public ArrangementRoomsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable checkIfRoomsAreBooked(int idArrangement, string idArticle, int id)
        {
            string query = string.Format(@" SELECT idArrangementBook
                                            FROM ArrangementBookArticles
                                            WHERE idArrangementBook IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook WHERE idArrangement='"+idArrangement+@"') 
                                            AND idArticle = '"+idArticle+@"' AND isContract = '0' AND id = '"+id+@"' and idRoom<>'' AND idRoom IS NOT NULL");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetLastRoomLetter(int idArrangement, string roomNumber)
        {
            string query = string.Format(@" SELECT idRoom
                                            FROM ArrangementRooms 
                                            WHERE idArrangement='" + idArrangement + @"'
                                            AND idRoom LIKE '"+roomNumber+"%'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllRoomsForArrangement(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement, idRoom, idArticle, isContract, id FROM ArrangementRooms WHERE idArrangement = '" + idArrangement.ToString() + "' ORDER BY SUBSTRING ( idRoom ,1 , 1 ), CAST( SUBSTRING ( idRoom ,2 , CHARINDEX('-',idRoom)-2 ) as INT), CAST( SUBSTRING ( idRoom ,CHARINDEX('-',idRoom) +1, LEN(idRoom) -  CHARINDEX('-',idRoom)+1) as INT)");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean Delete(ArrangementRoomsModel model, string nameForm, int idUser)
        {
             List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string query = string.Format(@"DELETE FROM ArrangementRooms
                    WHERE idArrangement = @idArrangement AND  @idArticle = @idArticle AND isContract = @isContract AND id=@id");


            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = model.idArrangement;

            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.idArticle;

            sqlParameter[2] = new SqlParameter("@isContract", SqlDbType.Bit);
            sqlParameter[2].Value = model.isContract;

            sqlParameter[3] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameter[3].Value = model.id;



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
            sqlParameter[4].Value = model.idArrangement.ToString() + "_" + model.idArticle + "_" + model.isContract.ToString() + "_" + model.id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement_ model.idArticle_model.isContract_model.id";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementRooms";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean DeleteInsertArrangamentRooms(BindingList<ArrangementArticalModel_Rooms> lista, int idArrangement, string nameForm, int idUser)
        {


            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();


            int roomNr = 0;
            int quantity=1;
            int nrOfArticles=1;
            foreach (ArrangementArticalModel_Rooms m in lista)
            {
                if (m.isChecked == true)
                {
                    DataTable dt = new DataTable();
                    int j= 0;
                    foreach (char c in alpha)
                    {
                        dt = new ArrangementRoomsDAO().GetLastRoomLetter(idArrangement, c.ToString());
                        if (dt != null)
                        {
                            if (dt.Rows.Count == 0)
                            {
                                roomNr = j;
                                
                                break;
                            }
                        }
                        else
                        {
                            roomNr = j;
                            break;
                        }
                        j++;
                    }

                    int num = 0;

                    for (int a = 0; a < m.nrArticle; a++)
                    {
                        string query = "";
                        if (a == 0)
                        {
                             query = string.Format(@"DELETE FROM ArrangementRooms
                               WHERE idArrangement = @idArrangement AND  @idArticle = @idArticle AND isContract = @isContract AND id=@id");


                            SqlParameter[] paremeters = new SqlParameter[4];

                            paremeters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                            paremeters[0].Value = idArrangement;

                            paremeters[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
                            paremeters[1].Value = m.idArticle;

                            paremeters[2] = new SqlParameter("@isContract", SqlDbType.Bit);
                            paremeters[2].Value = m.isContract;

                            paremeters[3] = new SqlParameter("@id", SqlDbType.Int);
                            paremeters[3].Value = m.id;


                            _query.Add(query);
                            sqlParameters.Add(paremeters);


                            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                            paremeters = new SqlParameter[8];


                             paremeters[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                             paremeters[0].Value = nameForm;

                             paremeters[1] = new SqlParameter("@idUser", SqlDbType.Int);
                             paremeters[1].Value = idUser;

                             paremeters[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                             paremeters[2].Value = DateTime.Now;

                             paremeters[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                             paremeters[3].Value = "D";

                             paremeters[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                             paremeters[4].Value = idArrangement.ToString() + "_" +m.idArticle.ToString() + "_"+m.isContract.ToString()+"_" +m.id.ToString();

                             paremeters[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                             paremeters[5].Value = "idArrangement_idArticle_isContract_id";

                             paremeters[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                             paremeters[6].Value = "ArrangementRooms";

                             paremeters[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                             paremeters[7].Value = "Delete/insert arrangament rooms";

                             _query.Add(query);
                             sqlParameters.Add(paremeters);

                        }

                        for (int i = 0; i < m.quantity; i++)
                        {
   
                            int bednr = num + 1;

                            
                            
                            

                            //string idRoom = alpha[roomNr] + bednr.ToString();

                            string idRoom = alpha[roomNr] + nrOfArticles.ToString() + "-" + quantity.ToString();

                            query = string.Format(@"INSERT INTO ArrangementRooms (idArrangement, idRoom, idArticle, isContract,id) 
                                  VALUES(@idArrangement, @idRoom, @idArticle, @isContract,@id)");


                            SqlParameter[]  paremeters = new SqlParameter[5];

                            paremeters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                            paremeters[0].Value = idArrangement;

                            paremeters[1] = new SqlParameter("@idRoom", SqlDbType.NVarChar);
                            paremeters[1].Value = idRoom;

                            paremeters[2] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
                            paremeters[2].Value = m.idArticle;

                            paremeters[3] = new SqlParameter("@isContract", SqlDbType.Bit);
                            paremeters[3].Value = m.isContract;

                            paremeters[4] = new SqlParameter("@id", SqlDbType.Int);
                            paremeters[4].Value = m.id;

                            _query.Add(query);
                            sqlParameters.Add(paremeters);

                            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                            paremeters = new SqlParameter[8];


                             paremeters[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                             paremeters[0].Value = nameForm;

                             paremeters[1] = new SqlParameter("@idUser", SqlDbType.Int);
                             paremeters[1].Value = idUser;

                             paremeters[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                             paremeters[2].Value = DateTime.Now;

                             paremeters[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                             paremeters[3].Value = "I";

                             paremeters[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                             paremeters[4].Value = idArrangement.ToString() + "_" +m.idArticle.ToString() + "_"+m.isContract.ToString()+"_" +m.id.ToString();

                             paremeters[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                             paremeters[5].Value = "idArrangement_idArticle_isContract_id";

                             paremeters[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                             paremeters[6].Value = "ArrangementRooms";

                             paremeters[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                             paremeters[7].Value = "Insert/delete arrangament rooms";

                             _query.Add(query);
                             sqlParameters.Add(paremeters);
                            num++;
                            
                            
                            if (quantity == m.quantity) 
                            {
                                quantity = 0;
                                nrOfArticles++;
                            }
                            quantity++;

                        }
                    }
                    alpha = new string(alpha).Replace(alpha[roomNr].ToString(), "").ToCharArray();
                }
            }
            
            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable  checkifRoomCanBeDeleted(ArrangementArticalModel_RoomsUpdate lista, int idArrangement)
        {
            string query = string.Format(@"SELECT DISTINCT SUBSTRING ( idRoom ,1 ,1) + CAST( SUBSTRING ( idRoom ,2 , Len(idRoom) - (Len(idRoom) - CHARINDEX('-',idRoom) + 2)) as nvarchar) as idRoom
					FROM ArrangementRooms
					WHERE SUBSTRING ( idRoom ,1 ,1) + CAST( SUBSTRING ( idRoom ,2 , Len(idRoom) - (Len(idRoom) - CHARINDEX('-',idRoom) + 2)) as nvarchar)  NOT IN (
					SELECT DISTINCT SUBSTRING ( idRoom ,1 ,1) + CAST( SUBSTRING ( idRoom ,2 , Len(idRoom) - (Len(idRoom) - CHARINDEX('-',idRoom) + 2)) as nvarchar) FROM ArrangementBookArticles aba LEFT JOIN ArrangementBook ab ON ab.idArrangementBook = aba.idArrangementBook 
					WHERE ab.idArrangement = '"+idArrangement+@"'
					and idArticle = '" + lista.idArticle + "') AND idArrangement = '" + idArrangement + @"' and idArticle = '" + lista.idArticle + "' ORDER BY SUBSTRING ( idRoom ,1 ,1)+CAST( SUBSTRING ( idRoom ,2 , Len(idRoom) - (Len(idRoom) - CHARINDEX('-',idRoom) + 2)) as nvarchar) desc");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean UpdateArrangamentRooms(List<ArrangementArticalModel_RoomsUpdate> lista, int idArrangement, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            int roomNr = 0;
            int quantity = 1;
            int nrOfArticles = 1;

            foreach (ArrangementArticalModel_RoomsUpdate m in lista)
            {
                if (m.nrArticle > 0)
                {
                    alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring("ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(m.nrLast.ToString().Substring(0, 1))).ToCharArray();
                    for (int a = Convert.ToInt32(m.nrLast.ToString().Substring(1)); a < m.nrArticle + Convert.ToInt32(m.nrLast.ToString().Substring(1)); a++)
                    {
                        string query = "";

                        for (int i = 0; i < m.quantity; i++)
                        {

                            string idRoom = alpha[roomNr].ToString() + a + "-" + quantity.ToString();

                            query = string.Format(@"INSERT INTO ArrangementRooms (idArrangement, idRoom, idArticle, isContract,id) 
                                  VALUES(@idArrangement, @idRoom, @idArticle, @isContract,@id)");


                            SqlParameter[] paremeters = new SqlParameter[5];

                            paremeters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                            paremeters[0].Value = idArrangement;

                            paremeters[1] = new SqlParameter("@idRoom", SqlDbType.NVarChar);
                            paremeters[1].Value = idRoom;

                            paremeters[2] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
                            paremeters[2].Value = m.idArticle;

                            paremeters[3] = new SqlParameter("@isContract", SqlDbType.Bit);
                            paremeters[3].Value = m.isContract;

                            paremeters[4] = new SqlParameter("@id", SqlDbType.Int);
                            paremeters[4].Value = m.id;

                            _query.Add(query);
                            sqlParameters.Add(paremeters);

                            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                            paremeters = new SqlParameter[8];


                             paremeters[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                             paremeters[0].Value = nameForm;

                             paremeters[1] = new SqlParameter("@idUser", SqlDbType.Int);
                             paremeters[1].Value = idUser;

                             paremeters[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                             paremeters[2].Value = DateTime.Now;

                             paremeters[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                             paremeters[3].Value = "I";

                             paremeters[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                             paremeters[4].Value = idArrangement.ToString() + "_" +m.idArticle.ToString() + "_"+m.isContract.ToString()+"_" +m.id.ToString();

                             paremeters[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                             paremeters[5].Value = "idArrangement_idArticle_isContract_id";

                             paremeters[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                             paremeters[6].Value = "ArrangementRooms";

                             paremeters[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                             paremeters[7].Value = "Update arrangament rooms";

                             _query.Add(query);
                             sqlParameters.Add(paremeters);
                            if (quantity == m.quantity)
                            {
                                quantity = 0;
                                nrOfArticles++;
                            }
                            quantity++;

                        }
                    }
                    alpha = new string(alpha).Replace(alpha[roomNr].ToString(), "").ToCharArray();
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt = checkifRoomCanBeDeleted(m, idArrangement);
                    if(dt!=null)
                        if(dt.Rows.Count>0)
                            for (int a = 0; a < -m.nrArticle; a++)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string query = "";

                                    query = string.Format(@"DELETE FROM ArrangementRooms
                                    WHERE idArrangement = @idArrangement AND  @idArticle = @idArticle AND 
                                    SUBSTRING ( idRoom ,1 ,1) + CAST( SUBSTRING ( idRoom ,2 , Len(idRoom) - (Len(idRoom) - CHARINDEX('-',idRoom) + 2)) as nvarchar) = @idRoom");

                                    SqlParameter[] paremeters = new SqlParameter[3];

                                    paremeters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                                    paremeters[0].Value = idArrangement;

                                    paremeters[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
                                    paremeters[1].Value = m.idArticle;

                                    paremeters[2] = new SqlParameter("@idRoom", SqlDbType.NVarChar);
                                    paremeters[2].Value = dt.Rows[i]["idRoom"].ToString();


                                    _query.Add(query);
                                    sqlParameters.Add(paremeters);

                                    query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

                                    paremeters = new SqlParameter[8];


                                    paremeters[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                                    paremeters[0].Value = nameForm;

                                    paremeters[1] = new SqlParameter("@idUser", SqlDbType.Int);
                                    paremeters[1].Value = idUser;

                                    paremeters[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                                    paremeters[2].Value = DateTime.Now;

                                    paremeters[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                                    paremeters[3].Value = "D";

                                    paremeters[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                                    paremeters[4].Value = idArrangement.ToString() + "_" +m.idArticle.ToString() + "_"+dt.Rows[i]["idRoom"].ToString();

                                    paremeters[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                                    paremeters[5].Value = "idArrangement_idArticle_idRoom";

                                    paremeters[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                                    paremeters[6].Value = "ArrangementRooms";

                                    paremeters[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                                    paremeters[7].Value = "Update arrangement rooms";

                                    _query.Add(query);
                                    sqlParameters.Add(paremeters);
                                    a++;
                                    if (a == -m.nrArticle)
                                        break;
                                }
                            }
                }
            }

            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable checkIfRoomAlready(int idArrangement, string idArticle)
        {
            string query = string.Format(@"SELECT idArticle FROM ArrangementRooms  WHERE idArrangement = '" + idArrangement.ToString() + "' AND idArticle = '" + idArticle + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable getNumberofBookedRooms(int idArrangement, string idArticle)
        {
            string query = string.Format(@"SELECT COUNT(Room) as Room FROM
                     (
                     SELECT DISTINCT SUBSTRING ( idRoom ,1 ,1) + CAST( SUBSTRING ( idRoom ,2 , Len(idRoom) - (Len(idRoom) - CHARINDEX('-',idRoom) + 2)) as nvarchar)  as Room
                     FROM ArrangementBook ab
                     INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                     WHERE idArrangement = '" + idArrangement.ToString() + "' AND idArticle = '" + idArticle + "') dd");

            return conn.executeSelectQuery(query, null);
        }
    }

}
