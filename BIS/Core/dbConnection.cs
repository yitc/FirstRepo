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


//zvanicna konekcija na ZIVU BAZU 
  //      private string connectionString = "Data Source=BUI-BIS;Initial Catalog=BISBuitenhof;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        // PRODUKCIONA BAZA  LOK.KONEKCIJA
    //NAJNOVIJA KONEKCIJA NA TEST BAZU ZA ZAPOSLENE U BUITENHOF-U
//  private string connectionString = "Data Source=84.105.81.217;Initial Catalog=BISBuitenhofTESTTEST;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
// private string connectionString = "Data Source=WIN-YITC-1;Initial Catalog=BISBuitenhof;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
//  private string connectionString = "Data Source=84.105.81.217;Initial Catalog=BISBuitenhofTESTTEST;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

 //     private string connectionString = "Data Source=84.105.81.217;Initial Catalog=BuitenhofBIS;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
// private string connectionString = "Data Source=84.105.81.217;Initial Catalog=BISYitc;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
 //  private string connectionString = "Data Source=WIN-YITC-1;Initial Catalog=BISYitc;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
        // LOKALNA BAZA U OFISU   
//    private string connectionString = "Data Source=109.122.102.222;Initial Catalog=BISBuitenhof;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
 //       private string connectionString = "Data Source=109.122.102.222;Initial Catalog=BuitenhofIMPORT;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        //==================================================================================================================================================
        //
       //     PRODUKCIONA  BAZA !!!!!!!  ZIVI PODACI !!!!
//  private string connectionString = "Data Source=WIN-YITC-1;Initial Catalog=BuitenhofBIS;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
  
        //=====================================================================================================================================================

//  private string connectionString = "Data Source=SAKICA\\SQLEXPRESS1;Initial Catalog=BISBuitenhofTESTTEST;Trusted_Connection=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
 // private string connectionString = "Data Source=SAKICA\\SQLEXPRESS1;Initial Catalog=MirceaTEST;Trusted_Connection=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
     private string connectionString = "Data Source=84.105.81.217;Initial Catalog=NetaTest;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        // Gogi privatna masina
// private string connectionString = "Data Source=G217\\SQLEXPRESS;Initial Catalog=BISYitc;Trusted_Connection=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
// private string connectionString = "Data Source=84.105.81.217;Initial Catalog=BISYitcTESTTEST;User ID=sa;Password=Yict#sys01#;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

        
        public dbConnection()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(connectionString);
        }

        public string DB
        {
            get { return conn.Database; }
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
        private SqlConnection closeConnection()
        {
            if (conn.State == ConnectionState.Open || conn.State ==
                        ConnectionState.Broken)
            {
                conn.Close();
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
                myCommand.Connection = closeConnection();
            }

            return obj;
        }
        public int executeInsertQuerySelectLastID(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            int obj = 0;
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;

                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);

                myAdapter.SelectCommand = myCommand;
                object retobj = myCommand.ExecuteScalar();

                if (retobj != null)
                    obj = Convert.ToInt32(retobj);
                else
                    obj = 0;
            }
            catch (SqlException e)
            {
                Console.Write(@"Error - Connection.executeUpdateQuery - Query: 
			" + _query + " \nException: " + e.StackTrace.ToString());
                // throw new Exception(e.Message);
                return 0;
            }
            finally
            {
                myCommand.Connection = closeConnection();
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
                myCommand.Connection = closeConnection();
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
                myCommand.Connection = closeConnection();
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
                myCommand.Connection = closeConnection();
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
                myCommand.Connection = closeConnection();
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
                myCommand.Connection = closeConnection();
            }

            return obj;
        }
        public bool executQueryTransaction(List<String> _query, List<SqlParameter[]> sqlParameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    for (int i = 0; i < _query.Count; i++)
                    {
                        command.CommandText = _query[i];
                       
                        if (sqlParameter != null)
                        {
                            if (sqlParameter[i] == null)
                            {
                                sqlParameter[i] = new SqlParameter[0];
                            }

                            command.Parameters.Clear();
                            command.Parameters.AddRange(sqlParameter[i]);
                        }
                        command.ExecuteNonQuery();
                    }
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Error - Connection.executQueryTransaction - Query: 
           " + _query + "Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("Message: {0}", ex.Message);

                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                        return false;
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        Console.WriteLine(@"Error - Connection.executQueryTransaction - Query: 
               " + _query + "Message: {0}", ex2.Message);
                        return false;
                    }

                }
            }
        }

        public int executQueryTransactionSelectLastID(List<String> _query, List<SqlParameter[]> sqlParameter)
        {
            int obj = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                command.Connection = connection;
                command.Transaction = transaction;
                object retobj = new object();
                try
                {
                    for (int i = 0; i < _query.Count; i++)
                    {
                        command.CommandText = _query[i];
                        if (sqlParameter[i] == null)
                        {
                            sqlParameter[i] = new SqlParameter[0];
                        }
                        if (sqlParameter != null)
                        {
                            command.Parameters.Clear();


                            command.Parameters.AddRange(sqlParameter[i]);
                        }
                        if (i == 0)
                        {
                            retobj = command.ExecuteScalar();
                        }
                        else
                        {
                            command.ExecuteNonQuery();
                        }
                    }

                    if (retobj != null)
                        obj = Convert.ToInt32(retobj);
                    else
                        obj = 0;
                    // Attempt to commit the transaction.
                    transaction.Commit();
                    return obj;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"Error - Connection.executQueryTransaction - Query: 
           " + _query + "Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("Message: {0}", ex.Message);

                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                        return obj;
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        Console.WriteLine(@"Error - Connection.executQueryTransaction - Query: 
               " + _query + "Message: {0}", ex2.Message);
                        return obj;
                    }

                }
            }
        }
          public List<int> executQueryTransactionSelectLastIDS(List<String> _query, List<SqlParameter[]> sqlParameter)
          {
              List<int> obj = new List<int>();
              using (SqlConnection connection = new SqlConnection(connectionString))
              {

                  connection.Open();

                  SqlCommand command = connection.CreateCommand();
                  SqlTransaction transaction;

                  // Start a local transaction.
                  transaction = connection.BeginTransaction("SampleTransaction");

                  command.Connection = connection;
                  command.Transaction = transaction;
                  object retobj = new object();
                  try
                  {
                      for (int i = 0; i < _query.Count; i++)
                      {
                          command.CommandText = _query[i];
                          if (sqlParameter != null)
                          {
                              command.Parameters.Clear();


                              command.Parameters.AddRange(sqlParameter[i]);
                          }
                          if (i != sqlParameter.Count-1)
                          {
                              retobj = command.ExecuteScalar();
                          }
                          else
                          {
                              command.ExecuteNonQuery();
                          }
                      }

                      if (retobj != null)
                          obj.Add(Convert.ToInt32(retobj));
                      // Attempt to commit the transaction.
                      transaction.Commit();
                      return obj;
                  }
                  catch (Exception ex)
                  {
                      Console.WriteLine(@"Error - Connection.executQueryTransaction - Query: 
           " + _query + "Commit Exception Type: {0}", ex.GetType());
                      Console.WriteLine("Message: {0}", ex.Message);

                      // Attempt to roll back the transaction. 
                      try
                      {
                          transaction.Rollback();
                          return obj;
                      }
                      catch (Exception ex2)
                      {
                          // This catch block will handle any errors that may have occurred 
                          // on the server that would cause the rollback to fail, such as 
                          // a closed connection.
                          Console.WriteLine(@"Error - Connection.executQueryTransaction - Query: 
               " + _query + "Message: {0}", ex2.Message);
                          return obj;
                      }

                  }
              }
          }
    }
}
