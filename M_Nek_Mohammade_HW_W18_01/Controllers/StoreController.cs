using M_Nek_Mohammade_HW_W18_01.DataAccessLayer;
using M_Nek_Mohammade_HW_W18_01.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace M_Nek_Mohammade_HW_W18_01.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreService _storeService;

        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }
        /*public IActionResult Index()
        {
            return View();
        }*/

        public async Task<IActionResult> Index(string nameFilter, string zipCodeFilter)
        {
            var stores = await _storeService.GetStoresAsync(nameFilter, zipCodeFilter);
            return View(stores);
        }

        public async Task<IActionResult> Details(int id)
        {
            var products = await _storeService.GetProductsByStoreIdAsync(id, true);
            ViewBag.StoreId = id;
            return View(products);
        }

       //تغییر details
        public async Task<IActionResult> SortProducts(int storeId, bool sortByPriceAsc)
        {
            var products = (await _storeService.GetProductsByStoreIdAsync(storeId, sortByPriceAsc));
            ViewBag.StoreId = storeId;
            return View ("Details", products);
        }

        public async Task<IActionResult> EditProduct(int productId)
        {
            var product = await _storeService.GetProductByIdAsync(productId);
            return View(product);
        }
        //details 
        //product.store.Id
        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _storeService.UpdateProductAsync(product);
                return RedirectToAction("Details", new { id = product.Store_Id });
            }
            return View(product);
        }
    }

}
