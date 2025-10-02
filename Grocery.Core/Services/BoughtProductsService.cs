
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository=groceryListItemsRepository;
            _groceryListRepository=groceryListRepository;
            _clientRepository=clientRepository;
            _productRepository=productRepository;
        }
        public List<BoughtProducts> Get(int? productId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> AggregateProductSales()
        {
            var groceryOccurances = _groceryListItemsRepository.GetAll();
            var productSales = new Dictionary<int, int>();

            foreach (var item in groceryOccurances)
            {
                if (productSales.ContainsKey(item.ProductId))
                    productSales[item.ProductId] += item.Amount;
                else
                    productSales[item.ProductId] = item.Amount;
            }
            return productSales;
        }

        public List<KeyValuePair<int, int>> GetTopProductSales(Dictionary<int, int> productSales, int topX)
        {
            return productSales
                .OrderByDescending(p => p.Value)
                .Take(topX)
                .ToList();
        }
    }
}
