using System.Data;
using Bakery.Models;

namespace Bakery.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        Task<int> CommandAsync(string formattableCommand);
        Task<DataSet> QueryAsync(string query);
    }

}