using System.Configuration;
using System.Data.SqlClient;

namespace sklad
{
    public static class DatabaseHelper
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["SkladDB"].ConnectionString;

        public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}