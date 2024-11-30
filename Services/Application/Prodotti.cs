using System.ComponentModel;
using System.Data;
using Bakery.Models;
using Bakery.Services.Infrastructure;

namespace Bakery.Services.Application
{
    public class Prodotti : IProdotti
    {
        private readonly IDatabaseAccessor db;

        public Prodotti(IDatabaseAccessor _db)
        {
            db = _db;
        }
        public async Task<List<Product>> GetProdotti()
        {
            List<Product> lista = new();
            DataSet ds;
            try
            {
                var query = "SELECT * FROM PRODUCTS";
                ds = await db.QueryAsync(query);
                var table = ds.Tables[0];
                lista = table.AsEnumerable().Select(x => new Product
                {
                    Id = Convert.ToInt32(x["Id"]),
                    Name = Convert.ToString(x["Name"]),
                    Description = Convert.ToString(x["Description"]),
                    Price = Convert.ToDecimal(x["Price"]),
                    ImageName = Convert.ToString(x["ImageName"]),
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }
        public async Task<Product?> FindAsync(int id)
        {
            Product? prodotto = null;
            DataSet ds;
            try
            {
                var query = $"SELECT * FROM PRODUCTS WHERE ID={id}";
                ds = await db.QueryAsync(query);
                var table = ds.Tables[0];
                if (!(table.Rows.Count == 0))
                {
                    prodotto = table.AsEnumerable().Select(x => new Product
                    {
                        Id = Convert.ToInt32(x["Id"]),
                        Name = Convert.ToString(x["Name"]),
                        Description = Convert.ToString(x["Description"]),
                        Price = Convert.ToDecimal(x["Price"]),
                        ImageName = Convert.ToString(x["ImageName"]),
                    }).First();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return prodotto;
        }
    }
}
