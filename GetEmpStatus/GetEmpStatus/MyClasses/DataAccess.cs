using System;
using System.Data;
using System.Data.SqlClient;

namespace GetEmpStatus.MyClasses
{
    public class DataAccess
    {
        private string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }

        public SqlDataReader ExecuteStoredProcedure(string storedProcedureName, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using (SqlCommand cmd = new SqlCommand(storedProcedureName, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddRange(parameters);
                
                return cmd.ExecuteReader();
            }
        }

    }
}
