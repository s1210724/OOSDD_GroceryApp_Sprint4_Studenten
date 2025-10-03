using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBoughtProductsService _boughtProductsService;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository, IBoughtProductsService boughtProductsService)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
            _boughtProductsService = boughtProductsService;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnProductId(int productId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.ProductId == productId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Get(int id)
        {
            return _groceriesRepository.Get(id);
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            return _groceriesRepository.Update(item);
        }

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            return MapToBestSellingProducts(_boughtProductsService.GetTopProductSales(_boughtProductsService.AggregateProductSales(), topX));
        }

        private List<BestSellingProducts> MapToBestSellingProducts(List<KeyValuePair<int, int>> topProductSales)
        {
            var bestSellingProducts = new List<BestSellingProducts>();
            int rank = 1;
            foreach (var product in topProductSales)
            {
                var p = _productRepository.Get(product.Key);
                if (p != null)
                {
                    bestSellingProducts.Add(new BestSellingProducts(p.Id, p.name, p.Stock, product.Value, rank++));
                }
            }
            return bestSellingProducts;
        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}
