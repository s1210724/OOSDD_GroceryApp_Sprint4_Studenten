using Grocery.Core.Services;
//using Moq;

namespace TestCore
{
    public class TestServices
    {
        private ProductService _productService;
        private Mock<Grocery.Core.Interfaces.Repositories.IProductRepository> _mockProductRepository;

        [SetUp]
        public void Setup()
        {
            _mockProductRepository = new Mock<Grocery.Core.Interfaces.Repositories.IProductRepository>();
            _productService = new ProductService(_mockProductRepository.Object);
        }

        [Test]
        public void GetProductById_ReturnsCorrectProduct()
        {
            // Arrange
            var expectedId = 1;
            var expectedName = "Apple";
            var expectedStock = 10;
            var product = new Grocery.Core.Models.Product(expectedId, expectedName, expectedStock);
            _mockProductRepository.Setup(r => r.Get(expectedId)).Returns(product);

            // Act
            var result = _productService.Get(expectedId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
        }
    }
}
