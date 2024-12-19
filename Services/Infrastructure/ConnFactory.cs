using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

public interface IConnectionFactory
{
    IDbConnection CreateConnection();
}
public class SqlConnectionFactory : IConnectionFactory
{

    readonly string _connectionString;
    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}