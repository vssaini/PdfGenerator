using System.Data;

namespace PdfGenerator.Contracts;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}