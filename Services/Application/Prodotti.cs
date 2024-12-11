using System.ComponentModel;
using System.Data;
using System.Globalization;
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

        public async Task Add(Product prod)
        {
            try
            {
                string usa = "en-US";
                var query = $"INSERT INTO PRODUCTS (Name, Price, Description, ImageName) VALUES('{prod.Name}',{prod.Price.ToString(new CultureInfo(usa))},'{prod.Description}','{prod.ImageName}');";

                var num = await db.CommandAsync(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Delete(int id)
        {

            try
            {
                var query = $"DELETE FROM PRODUCTS WHERE ID={id};";
                var num = await db.CommandAsync(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<int> Update(Product prod)
        {
            var num = 0;
            try
            {
                string usa = "en-US";
                var query = $"UPDATE PRODUCTS SET Name='{prod.Name}', Price={prod.Price.ToString(new CultureInfo(usa))}, Description='{prod.Description}', ImageName='{prod.ImageName}' WHERE ID={prod.Id}  ;";
                num = await db.CommandAsync(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return num;
        }
    }
}
