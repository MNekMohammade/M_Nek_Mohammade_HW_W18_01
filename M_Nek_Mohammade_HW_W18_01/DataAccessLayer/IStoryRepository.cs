using M_Nek_Mohammade_HW_W18_01.ServiceLayer;
namespace M_Nek_Mohammade_HW_W18_01.DataAccessLayer
{
       public interface IStoreRepository
        {
            Task<IEnumerable<Store>> GetAllStoresAsync(string nameFilter, string zipCodeFilter);
            Task<IEnumerable<Product>> GetProductsByStoreIdAsync(int storeId, bool sortByPriceAsc);
            Task<Product> GetProductByIdAsync(int productId);
            Task UpdateProductAsync(Product product);
        }

}
