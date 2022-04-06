using Insurance.Api.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Insurance.Api.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDto> GetProduct(int productId);
        public Task<ProductTypeDto> GetProductType(ProductDto productDto);
        public Task<List<ProductDto>> GetProducts(IEnumerable<int> productsIds);
        public Task<List<ProductTypeDto>> GetProductTypes(IEnumerable<ProductDto> productDtoList);
    }
}
