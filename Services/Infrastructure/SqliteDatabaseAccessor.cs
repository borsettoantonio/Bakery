using System.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Configuration;
using Bakery.Models;

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

        public async Task<DataSet> QueryAsync(string query)
        {
            using SqliteConnection conn = await GetOpenedConnection();
            using SqliteCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            using var reader = await cmd.ExecuteReaderAsync();

            var dataSet = new DataSet();
            // ciclo per leggere più query
            do
            {
                var dataTable = new DataTable();
                dataSet.Tables.Add(dataTable);
                dataTable.Load(reader);
            } while (!reader.IsClosed);
            /*
            while (reader.Read())
            {
                string title = (string)reader["Title"];
                ....
            }
            */
            return dataSet;
        }

        private async Task<SqliteConnection> GetOpenedConnection()
        {
            var conn = new SqliteConnection(config.GetSection("ConnectionStrings").GetValue<string>("Default"));
            //var conn2 = new SqliteConnection(config.GetSection("ConnectionStrings")["Default"]);
            await conn.OpenAsync();
            return conn;
        }
    }
}