using System.Data;
using Microsoft.Data.SqlClient;
using PdfGenerator.Contracts;

namespace PdfGenerator.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();

        return connection;
    }
}