using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
    public class FiltersLabelsDAO
    {

        private dbConnection conn;

        public FiltersLabelsDAO()
        {
            conn = new dbConnection();
        }
        public DataTable GetAllFiltersLabels(string language)
        {
            string query = string.Format(
                @"SELECT fl.idFilter as Id, CASE WHEN s.stringValue IS NOT NULL THEN  s.stringValue ELSE fl.nameFilter END as Name, 'Filter'  AS Type,'' AS nameMenu,'' AS uniques,
                  '-1' as IDLabelUnique
                  FROM Filters fl
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = fl.nameFilter
                  
                  UNION
                  SELECT lb.idLabel as Id,CASE WHEN s.stringValue IS NOT NULL THEN  s.stringValue ELSE lb.nameLabel END as Name, 'Label'  AS Type, m.nameMenu AS nameMenu, 
                                         CASE WHEN lb.id IS NOT NULL THEN  'unique' ELSE '' END as uniques,
                                         CASE WHEN lb.id IS NOT NULL THEN  lb.id ELSE '-1' END as IDLabelUnique
                  FROM Labels lb
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = lb.nameLabel
                  LEFT OUTER JOIN Menu m on lb.idMenu=m.idMenu
                  
                  
                  ORDER BY Type");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.VarChar);
            sqlParameters[0].Value = language;

            return conn.executeSelectQuery(query, null);
        }
        public Boolean UpdateFilterName(int idFilter, string nameFilter, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Filters
                SET nameFilter = @nameFilter
	        	WHERE idFilter = @idFilter");
            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idFilter", SqlDbType.Int);
            sqlParameter[0].Value = idFilter;

            sqlParameter[1] = new SqlParameter("@nameFilter", SqlDbType.VarChar);
            sqlParameter[1].Value = nameFilter;

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
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idFilter;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFilter";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Filters";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update filter name";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public Boolean UpdateLabelName(int idLabel, string nameLabel, int idMenu, int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Labels
                SET nameLabel = @nameLabel, idMenu=@idMenu, id=@id
	        	WHERE idLabel = @idLabel");
            SqlParameter[] sqlParameter = new SqlParameter[4];
            sqlParameter[0] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameter[0].Value = idLabel;

            sqlParameter[1] = new SqlParameter("@nameLabel", SqlDbType.VarChar);
            sqlParameter[1].Value = nameLabel;

            sqlParameter[2] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameter[2].Value = idMenu;

            sqlParameter[3] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameter[3].Value = (id == -1) ? SqlInt32.Null : id;

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
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idLabel;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLabel";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Labels";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update labels name";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertFilter(int idFilter, string nameFilter, int sortFilter, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   Filters(idFilter,nameFilter, sortFilter)
                                    VALUES(@idFilter,@nameFilter, @sortFilter)");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idFilter", SqlDbType.Int);
            sqlParameter[0].Value = idFilter;


            sqlParameter[1] = new SqlParameter("@nameFilter", SqlDbType.NVarChar);
            sqlParameter[1].Value = Convert.ToString(nameFilter.Trim());

            sqlParameter[2] = new SqlParameter("@sortFilter", SqlDbType.Int);
            sqlParameter[2].Value = sortFilter;


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
            sqlParameter[4].Value = idFilter;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFilter";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Filters";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert filter";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool InsertLabel(int idLabel, string nameLabel, int idMenu, int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   Labels(idLabel,nameLabel,idMenu,id)
                                    VALUES(@idLabel,@nameLabel,@idMenu, @id)");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameter[0].Value = idLabel;

            sqlParameter[1] = new SqlParameter("@nameLabel", SqlDbType.NVarChar);
            sqlParameter[1].Value = Convert.ToString(nameLabel.Trim());

            sqlParameter[2] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameter[2].Value = idMenu;

            sqlParameter[3] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameter[3].Value = (id == -1) ? SqlInt32.Null : id;


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
            sqlParameter[4].Value = idLabel;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLabel";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Labels";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert labels";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public DataTable GetAllFilters()
        {
            string query = string.Format(
               @"SELECT Top 1 idFilter, nameFilter, sortFilter
                  FROM Filters order by  sortFilter desc");

            return conn.executeSelectQuery(query, null);

        }

        public bool DeleteFilter(int idFilter, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  Filters WHERE idFilter = @idFilter");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idFilter", SqlDbType.Int);
            sqlParameter[0].Value = idFilter;

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
            sqlParameter[4].Value = idFilter;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idFilter";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Filters";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete filters";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteLabel(int idLabel, int id, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query ="";
            SqlParameter[] sqlParameter = new SqlParameter[1];
            int idKey = 0;
            string nameKey = "";
            if (idLabel > 0)
            {
                 query= string.Format(@"DELETE FROM  Labels WHERE idLabel = '" + idLabel + "'");

                sqlParameter[0] = new SqlParameter("@idLabel", SqlDbType.Int);
                sqlParameter[0].Value = idLabel;

                idKey = idLabel;
                nameKey = "idLabel";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
            }

            else
            {
                query = string.Format(@"DELETE FROM  Labels WHERE id = '" + id + "'");

                sqlParameter[0] = new SqlParameter("@id", SqlDbType.Int);
                sqlParameter[0].Value = id;

                idKey = id;
                nameKey = "id";

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
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idKey;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = nameKey;

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Labels";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete labels";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable GetLastIdFilter()
        {
            string query = string.Format(
               @"SELECT Top 1 idFilter, nameFilter, sortFilter
                  FROM Filters order by  idFilter desc");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetLastIdLabel()
        {
            string query = string.Format(
               @"SELECT Top 1 idLabel, nameLabel
                  FROM Labels order by  idLabel desc");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable isInFilter()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idFilter IS NOT NULL THEN idFilter  ELSE '-1' END AS ID
                           FROM  ClientFilter
                           UNION
                            SELECT distinct CASE WHEN idFilter IS NOT NULL THEN idFilter  ELSE '-1' END AS ID
                           FROM ContactPersonFilter
                           UNION
                           SELECT distinct CASE WHEN idFilter IS NOT NULL THEN idFilter  ELSE '-1' END AS ID
                           FROM EmployeeFilter
                           UNION
                           SELECT distinct CASE WHEN idFilter IS NOT NULL THEN idFilter  ELSE '-1' END AS ID
                           FROM UsersFilter
                          ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable isInLabel()
        {
            string query = string.Format(@"
                           SELECT distinct CASE WHEN idLabel IS NOT NULL THEN idLabel  ELSE '-1' END AS ID
                           FROM  ClientLabel
                           UNION
                            SELECT distinct CASE WHEN idLabel IS NOT NULL THEN idLabel  ELSE '-1' END AS ID
                           FROM ContactPersonLabel
                           UNION
                           SELECT distinct CASE WHEN idLabel IS NOT NULL THEN idLabel  ELSE '-1' END AS ID
                           FROM EmployeeLabel
                           UNION
                           SELECT distinct CASE WHEN idLabel IS NOT NULL THEN idLabel  ELSE '-1' END AS ID
                           FROM UsersLabel
                          ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetNameMenu()
        {
            string query = string.Format(
                @"SELECT idMenu,nameMenu
                  FROM Menu
                 WHERE isGrid='true' 
                ");


            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetIDMenu(string nameMenu)
        {
            string query = string.Format(
                @"SELECT idMenu
                  FROM Menu
                  WHERE nameMenu=@nameMenu
                ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@nameMenu", SqlDbType.VarChar);
            sqlParameters[0].Value = nameMenu;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        // NOVO:


        public DataTable GetIdLabelForExistName(string nameLabel)
        {
            string query = string.Format(
                @"SELECT TOP (1) CASE WHEN id is not null then id else '-1' end  as idLabel
                  FROM Labels
                  WHERE nameLabel LIKE @nameLabel ORDER BY id DESC
                ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@nameLabel", SqlDbType.VarChar);
            sqlParameters[0].Value = nameLabel;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable ForExistLabelidIsExist(string nameLabel)
        {
            string query = string.Format(
                @"SELECT CASE WHEN id is not NULL then id else '-1' end as idLabel
                  FROM Labels
                  WHERE nameLabel LIKE @nameLabel ORDER BY id DESC
                ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@nameLabel", SqlDbType.VarChar);
            sqlParameters[0].Value = nameLabel;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable NameLabelIsExist(string nameLabel)
        {
            string query = string.Format(
                @"SELECT CASE WHEN idLabel IS NOT NULL then idLabel ELSE '-1' END as idLabel
                  FROM Labels
                  WHERE nameLabel LIKE @nameLabel  
                ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@nameLabel", SqlDbType.VarChar);
            sqlParameters[0].Value = nameLabel;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetIdLabelForNotExistName()
        {
            string query = string.Format(
                @"SELECT TOP (1) CASE WHEN id is not null then id else '-1' end  as idLabel
                  FROM Labels Order by id DESC
                 
                ");

     

            return conn.executeSelectQuery(query, null);
        }
        //NOVO BRISANJE
        public DataTable checkIsInArrangementLabel(int idLabel)
        {
            string query = string.Format(@"
                SELECT * FROM ArrangementLabel 
                WHERE idLabel = '" + idLabel + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInArrangementLabelFirst(int idLabel)
        {
            string query = string.Format(@"
                SELECT * FROM ArrangementLabelFirst 
                WHERE idLabel = '" + idLabel + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInClientLabel(int idLabel)
        {
            string query = string.Format(@"
                SELECT * FROM ClientLabel 
                WHERE idLabel = '" + idLabel + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInContactPersonLabel(int idLabel)
        {
            string query = string.Format(@"
                SELECT * FROM ContactPersonLabel 
                WHERE idLabel = '" + idLabel + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInEmployeeLabel(int idLabel)
        {
            string query = string.Format(@"
                SELECT * FROM EmployeeLabel 
                WHERE idLabel = '" + idLabel + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInUserLabel(int idLabel)
        {
            string query = string.Format(@"
                SELECT * FROM UsersLabel 
                WHERE idLabel = '" + idLabel + "'");

            return conn.executeSelectQuery(query, null);
        }

        // FILTERI
        public DataTable checkIsInArrangementFilter(int idFilter)
        {
            string query = string.Format(@"
                SELECT * FROM ArrangementFilter
                WHERE idFilter = '" + idFilter + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInClientFilter(int idFilter)
        {
            string query = string.Format(@"
                SELECT * FROM ClientFilter 
                WHERE idFilter = '" + idFilter + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInContactPersonFilter(int idFilter)
        {
            string query = string.Format(@"
                SELECT * FROM ContactPersonFilter
                WHERE idFilter = '" + idFilter + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInEmployeeFilter(int idFilter)
        {
            string query = string.Format(@"
                SELECT * FROM EmployeeFilter
                WHERE idFilter = '" + idFilter + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInUserFilter(int idFilter)
        {
            string query = string.Format(@"
                SELECT * FROM UsersFilter
                WHERE idFilter = '" + idFilter + "'");

            return conn.executeSelectQuery(query, null);
        }
    }
}
