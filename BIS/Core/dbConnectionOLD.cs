using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace BIS.Core
{
    public class dbConnection
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;

        public dbConnection()
        {
            myAdapter = new SqlDataAdapter();
       //  conn = new SqlConnection("Data Source=WIN-YITC-1;Initial Catalog=BISBuitenhof;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
  // conn = new SqlConnection("Data Source=WIN-YITC-1;Initial Catalog=BISWielewaal;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
   // conn = new SqlConnection("Data Source=109.122.102.222;Initial Catalog=BISWielewaal;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
   conn = new SqlConnection("Data Source=109.122.102.222;Initial Catalog=BISBuitenhof;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
        }

        public string Conn
        {
            get { return conn.ConnectionString; }
        }

        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State ==
                        ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
        public int GetLastTableID(string tablename)
        {                                    
            SqlCommand myCommand = new SqlCommand();
            int obj = 0;
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = "SELECT IDENT_CURRENT('"+ tablename +"')";             
                myAdapter.SelectCommand = myCommand;
                object retobj = myCommand.ExecuteScalar();
                obj = Convert.ToInt32(retobj);
            }
            catch (SqlException e)
            {
                Console.Write(@"Error - Connection.executeUpdateQuery - Query: 
			" + "SELECT IDENT_CURRENT('"+ tablename +"')" + " \nException: " + e.StackTrace.ToString());
                // throw new Exception(e.Message);
                return 0;
            }
            finally
            {
            }

            return obj;
        }
        public DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myAdapter.SelectCommand = new SqlCommand();
                myAdapter.SelectCommand.Connection = openConnection();
                myAdapter.SelectCommand.CommandText = _query;

                if (sqlParameter != null)
                    myAdapter.SelectCommand.Parameters.AddRange(sqlParameter);
                myAdapter.SelectCommand.CommandTimeout = 800000;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                Console.Write(@"Error - Connection.executeSelectQuery - Query: 
			" + _query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {

            }
            return dataTable;


        }

        public bool executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;

                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
                            

                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write(@"Error - Connection.executeInsertQuery - Query: 
			" + _query + " \nException: \n" + e.StackTrace.ToString());
                // throw new Exception(e.Message);
                return false;
            }
            finally
            {
            }
            return true;
        }

        public bool executeUpdateQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                Console.Write(@"Error - Connection.executeUpdateQuery - Query: 
			" + _query + " \nException: " + e.StackTrace.ToString());
                // throw new Exception(e.Message);
                return false;
            }
            finally
            {
            }
            return true;
        }

        public bool executeDeleteQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write(@"Error - Connection.executeUpdateQuery - Query: 
			" + _query + " \nException: " + e.StackTrace.ToString());
                //throw new Exception(e.Message);
                return false;
            }
            finally
            {
            }
            return true;
        }

        public object executeScalarQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            object obj = null;
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                obj = myCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                Console.Write(@"Error - Connection.executeUpdateQuery - Query: 
			" + _query + " \nException: " + e.StackTrace.ToString());
                // throw new Exception(e.Message);
                return false;
            }
            finally
            {
            }

            return obj;
        }

        #region CRUD

        public void InsertObj<T>(T obj)
        {
            CultureInfo ci = new CultureInfo("en-US");
            System.Reflection.PropertyInfo[] npi = obj.GetType().GetProperties();

            string[] nName = obj.GetType().FullName.Split('.');
            string sName = obj.GetType().FullName.Split('.')[nName.Length - 1].Replace("Model", "");
            string sQuery = "insert into " + sName + " (";

            Int32 i = 0;
            foreach (System.Reflection.PropertyInfo pi in npi)
                if (i++ == 0)
                    continue;
                else
                    sQuery += pi.Name + ", ";

            i = 0;
            sQuery = sQuery.Substring(0, sQuery.Length - 2) + ") values (";
            foreach (System.Reflection.PropertyInfo pi in npi)
            {
                if (i++ == 0)
                    continue;
                else
                    switch (pi.PropertyType.ToString().ToLower())
                    {
                        case "system.timespan":
                        case "system.nullable`1[system.timespan]":
                            sQuery += "'" + pi.GetValue(obj) + "', ";
                            break;
                        case "system.nullable`1[system.datetime]":
                        case "system.datetime":
                            string sDate = Convert.ToDateTime(pi.GetValue(obj).ToString(), System.Threading.Thread.CurrentThread.CurrentCulture).ToString(ci);
                            sQuery += "'" + sDate + "', ";
                            break;
                        case "system.string":
                            sQuery += "'" + pi.GetValue(obj) + "', ";
                            break;
                        case "system.double":
                        case "system.single":
                        case "system.decimal":
                        case "system.int64":
                        case "system.int16":
                        case "system.int32":
                        case "system.int":
                        case "system.nullable`1[system.int32]":
                        case "system.nullable`1[system.int16]":
                        case "system.nullable`1[system.int64]":
                        case "system.nullable`1[system.decimal]":
                        case "system.nullable`1[system.single]":
                        case "system.nullable`1[system.double]":
                        case "system.nullable`1[system.int]":
                            if (pi.GetValue(obj) == null)
                                sQuery = sQuery.Replace(pi.Name + ", ", "");
                            else
                                sQuery += pi.GetValue(obj) + ", ";
                            break;
                        case "system.bool":
                        case "system.boolean":
                            sQuery += ((bool)pi.GetValue(obj) ? "1" : "0") + ", ";
                            break;
                        default:
                            sQuery = sQuery.Replace(pi.Name + ", ", "");
                            sQuery = sQuery.Replace(", " + pi.Name + ")", ")");
                            sQuery = sQuery.Replace("(" + pi.Name + ", ", "(");
                            break;
                    }
            }

            sQuery = sQuery.Substring(0, sQuery.Length - 2) + ")";

            bool bRes = executeInsertQuery(sQuery, null);
        }

        public void DeleteObj<T>(T obj)
        {
            System.Reflection.PropertyInfo[] npi = obj.GetType().GetProperties();

            string[] nName = obj.GetType().FullName.Split('.');
            string sName = obj.GetType().FullName.Split('.')[nName.Length - 1].Replace("Model", "");
            string sQuery = "delete from " + sName + " where ";

            Int32 i = 0;
            string sFirst = "", sVal = "";

            foreach (System.Reflection.PropertyInfo pi in npi)
            {
                if (i == 0)
                {
                    i++;
                    sFirst = pi.Name;
                    sVal = pi.GetValue(obj).ToString();
                    break;
                }
            }
            sQuery += sFirst + " = " + sVal;

            bool bRes = executeUpdateQuery(sQuery, null);
        }

        public void UpdateObj<T>(T obj)
        {
            CultureInfo ci = new CultureInfo("en-US");
            System.Reflection.PropertyInfo[] npi = obj.GetType().GetProperties();

            string[] nName = obj.GetType().FullName.Split('.');
            string sName = obj.GetType().FullName.Split('.')[nName.Length - 1].Replace("Model", "");
            string sQuery = "update " + sName + " set ";

            Int32 i = 0;
            string sFirst = "", sVal = "";

            foreach (System.Reflection.PropertyInfo pi in npi)
            {
                if (i == 0)
                {
                    i++;
                    sFirst = pi.Name;
                    sVal = pi.GetValue(obj).ToString();
                }
                else
                {
                    if (pi.GetValue(obj) != null)
                        switch (pi.PropertyType.ToString().ToLower())
                        {
                            case "system.timespan":
                            case "system.nullable`1[system.timespan]":
                                sQuery += pi.Name + " = '" + pi.GetValue(obj) + "', ";
                                break;
                            case "system.nullable`1[system.datetime]":
                            case "system.datetime":
                                string sDate = Convert.ToDateTime(pi.GetValue(obj).ToString(), System.Threading.Thread.CurrentThread.CurrentCulture).ToString(ci);
                                sQuery += pi.Name + " = '" + sDate + "', ";
                                break;
                            case "system.string":
                                sQuery += pi.Name + " = '" + pi.GetValue(obj) + "', ";
                                break;
                            case "system.double":
                            case "system.single":
                            case "system.decimal":
                            case "system.int64":
                            case "system.int16":
                            case "system.int32":
                            case "system.nullable`1[system.int32]":
                            case "system.nullable`1[system.int16]":
                            case "system.nullable`1[system.int64]":
                            case "system.nullable`1[system.decimal]":
                            case "system.nullable`1[system.single]":
                            case "system.nullable`1[system.double]":
                            case "system.nullable`1[system.int]":
                            case "system.int":
                                if (pi.GetValue(obj).ToString() != "0")
                                    sQuery += pi.Name + " = " + pi.GetValue(obj) + ", ";
                                break;
                            case "system.bool":
                            case "system.boolean":
                                sQuery += pi.Name + " = " + (pi.GetValue(obj).ToString() == "True" ? "1" : "0") + ", ";
                                break;
                        }
                }
            }

            sQuery = sQuery.Substring(0, sQuery.Length - 2) + " where " + sFirst + " = " + sVal;

            bool bRes = executeUpdateQuery(sQuery, null);
        }

        #endregion

    }
}
