using System.Data;
using Bakery.Models;

namespace Bakery.Services.Application
{
    public interface IInitDb
    {
        Task InitDatabase();
    }

}