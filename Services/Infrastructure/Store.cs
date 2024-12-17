namespace Bakery.Services.Infrastructure
{
    public class Store
    {
        public string dato { get; set; } = "Vuoto";
        private readonly ILogger<SqliteDatabaseAccessor> logger;

        public Store(ILogger<SqliteDatabaseAccessor> logger)
        {
            this.logger = logger;
        }

    }
}