using System.Data;

namespace Bakery.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        //Task<DataSet> QueryAsync(FormattableString query);
        //Task<T> QueryScalarAsync<T>(FormattableString formattableQuery);
        Task<int> CommandAsync(string formattableCommand);
    }

}