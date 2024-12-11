using Bakery.Models;

namespace Bakery.Services.Application
{
    public interface IProdotti
    {
        Task<List<Product>> GetProdotti();
        Task<Product?> FindAsync(int id);
        Task Add(Product prod);
        Task Delete(int id);
        Task<int> Update(Product prod);
    }

}