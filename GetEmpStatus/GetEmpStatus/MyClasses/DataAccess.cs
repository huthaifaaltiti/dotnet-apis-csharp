using System;
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
    }
}
