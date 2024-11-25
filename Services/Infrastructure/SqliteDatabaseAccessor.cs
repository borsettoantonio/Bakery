using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Configuration;

namespace Bakery.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        private readonly IConfiguration config;
        private readonly ILogger<SqliteDatabaseAccessor> logger;

        public SqliteDatabaseAccessor(IConfiguration config, ILogger<SqliteDatabaseAccessor> logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public async Task<int> CommandAsync(string comando)
        {
            try
            {
                using SqliteConnection conn = await GetOpenedConnection();
                using SqliteCommand cmd = conn.CreateCommand();
                cmd.CommandText = comando;
                int affectedRows = await cmd.ExecuteNonQueryAsync();
                return affectedRows;
            }
            catch (SqliteException exc)
            {
                Console.WriteLine(exc.Message);
                return 0;
            }
        }

        private async Task<SqliteConnection> GetOpenedConnection()
        {
            var conn = new SqliteConnection(config.GetSection("ConnectionStrings").GetValue<string>("Default"));
            await conn.OpenAsync();
            return conn;
        }
    }
}