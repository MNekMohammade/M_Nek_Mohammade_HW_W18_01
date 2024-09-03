using M_Nek_Mohammade_HW_W18_01.DataAccessLayer;

namespace M_Nek_Mohammade_HW_W18_01.ServiceLayer
{
    public class StoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public Task<IEnumerable<Store>> GetStoresAsync(string nameFilter, string zipCodeFilter)
        {
            return _storeRepository.GetAllStoresAsync(nameFilter, zipCodeFilter);
        }

        // StoreService.cs
        public async Task<IEnumerable<Product>> GetProductsByStoreIdAsync(int storeId, bool sortByPriceAsc)
        {
            return await _storeRepository.GetProductsByStoreIdAsync(storeId, sortByPriceAsc);
        }


        public Task<Product> GetProductByIdAsync(int productId)
        {
            return _storeRepository.GetProductByIdAsync(productId);
        }

        public Task UpdateProductAsync(Product product)
        {
            return _storeRepository.UpdateProductAsync(product);
        }
    }

}
