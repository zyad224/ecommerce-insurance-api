using Insurance.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ProductDto> GetProduct(int productID);
        public Task<ProductTypeDto> GetProductType(ProductDto productDto);
        public Task<List<ProductDto>> GetProducts(IEnumerable<int> productsIDS);
        public Task<List<ProductTypeDto>> GetProductTypes(IEnumerable<ProductDto> productDtoList);



    }
}
