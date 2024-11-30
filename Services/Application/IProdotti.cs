using Bakery.Models;

namespace Bakery.Services.Application
{
    public interface IProdotti
    {
        Task<List<Product>> GetProdotti();
        Task<Product?> FindAsync(int id);
    }

}