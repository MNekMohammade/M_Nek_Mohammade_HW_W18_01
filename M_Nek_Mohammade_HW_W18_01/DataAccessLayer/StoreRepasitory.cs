using M_Nek_Mohammade_HW_W18_01.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace M_Nek_Mohammade_HW_W18_01.DataAccessLayer
{
    public class StoreRepository : IStoreRepository
    {
        private readonly string _connectionString;

        public StoreRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Store>> GetAllStoresAsync(string nameFilter, string zipCodeFilter)
        {
            using (var db = Connection)
            {
                var query = "SELECT * FROM sales.stores WHERE store_name LIKE @NameFilter AND zip_code LIKE @ZipCodeFilter";
                return await db.QueryAsync<Store>(query, new { NameFilter = $"%{nameFilter}%", ZipCodeFilter = $"%{zipCodeFilter}%" });
            }
        }

        // StoreRepository.cs
        /*public async Task<IEnumerable<Product>> GetProductsByStoreIdAsync(int storeId, bool sortByPriceAsc)
        {
            using (var db = Connection)
            {
                var query = $"SELECT * FROM production.products WHERE Store_Id = @Store_Id ORDER BY List_Price {(sortByPriceAsc ? "ASC" : "DESC")}";
                return await db.QueryAsync<Product>(query, new { Store_Id = storeId });
            }
        }*/
        public async Task<IEnumerable<Product>> GetProductsByStoreIdAsync(int storeId, bool sortByPriceAsc)
{
                using (var db = Connection)
                {
                    var query = @"
                        SELECT p.*
                        FROM production.products p
                        INNER JOIN production.stocks s ON p.product_id = s.product_id
                        WHERE s.store_id = @Store_Id
                        ORDER BY p.list_price " + (sortByPriceAsc ? "ASC" : "DESC");
                //edit Store_Id
                    return await db.QueryAsync<Product>(query, new { Store_Id = storeId });
                }
            }


        public async Task<Product> GetProductByIdAsync(int productId)
        {
            using (var db = Connection)
            {
                //edit Product_Id
                var query = "SELECT * FROM Production.Products WHERE Product_Id = @ProductId";
                return await db.QueryFirstOrDefaultAsync<Product>(query, new { ProductId = productId });
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            using (var db = Connection)
            {
                var query = "UPDATE Products SET Name = @Name, Model_Year = @Model_Year, Price = @Price WHERE ProductId = @ProductId";
                await db.ExecuteAsync(query, product);
            }
        }
    }

}
