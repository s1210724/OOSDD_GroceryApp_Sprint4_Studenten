
using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IBoughtProductsService
    {
        public List<BoughtProducts> Get(int productId);
        public Dictionary<int, int> AggregateProductSales();
        public BoughtProducts CreateBoughtProduct(int productId, int groceryListId);
        public List<KeyValuePair<int, int>> GetTopProductSales(Dictionary<int, int> productSales, int topX);
    }
}
