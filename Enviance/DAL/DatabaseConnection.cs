using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    class DatabaseConnection
    {
        SqlConnection connection;

        public DatabaseConnection()
        {
            try
            {   
                OpenConnection(ConfigurationManager.ConnectionStrings["EnvianceDB"].ConnectionString);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Null Reference exception thrown");
                throw ex;
            }
            catch (Exception ex)
            {

            }
        }

        private void OpenConnection(string conString)
        {
            connection = new SqlConnection(conString);
            connection.Open();
        }

        public int ExecuteNonQuery(string sQuery, SqlParameter[] parameter)
        {
            int iReturn = 0;
            SqlTransaction sqlTransaction = BeginTransaction("Transaction");
            try
            {
                if (String.Empty != sQuery)
                {
                    SqlCommand command = new SqlCommand(sQuery, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = sqlTransaction;
                    if (parameter != null)
                    {
                        foreach (SqlParameter p in parameter)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    iReturn = command.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                iReturn = -1;
                sqlTransaction.Rollback();
            }

            return iReturn;
        }

        public DataTable ExecuteReader(string sQuery, SqlParameter[] parameter)
        {
            
            DataTable table = new DataTable();
            try
            {
                if (String.Empty != sQuery)
                {
                    SqlCommand command = new SqlCommand(sQuery, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    
                    if (parameter != null)
                    {
                        foreach (SqlParameter p in parameter)
                        {
                            command.Parameters.Add(p);
                        }
                    }
                    table.Load(command.ExecuteReader());
                    
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return table;
            }
            return null;
        }

        public SqlTransaction BeginTransaction(String transactionName)
        {
            return connection.BeginTransaction(transactionName);
        }
    }
}
