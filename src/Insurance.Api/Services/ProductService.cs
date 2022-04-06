using Insurance.Api.Dtos;
using Insurance.Api.Services.Interfaces;
using Insurance.Domain.DomainExceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Insurance.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient { BaseAddress = new Uri(_configuration["ProductApi:URL"]) };

        }
        public async Task<ProductDto> GetProduct(int productId)
        {
            if (productId == 0)
                throw new InvalidProductException("Invalid ProductId");

            var result = await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProduct"], productId));
            var product = JsonConvert.DeserializeObject<ProductDto>(result.Content.ReadAsStringAsync().Result);     
            return product;
        }
        public async Task<List<ProductDto>> GetProducts(IEnumerable<int> productsIds)
        {
            if((productsIds == null) || (!productsIds.Any()))
                throw new InvalidProductException("Invalid ProductId List");

            var result = await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProducts"]));
            var productList = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(result.Content.ReadAsStringAsync().Result);
            var filteredProductDtoList = productList.Where(pdto => productsIds.Any(productsIds => pdto.Id== productsIds)).ToList();
            return filteredProductDtoList;
        }
        public async Task<ProductTypeDto> GetProductType(ProductDto productDto)
        {
            if (productDto == null)
                throw new InvalidProductException("Invalid ProductDto");

            var result = await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProductType"], productDto.ProductTypeId));
            var productType = JsonConvert.DeserializeObject<ProductTypeDto>(result.Content.ReadAsStringAsync().Result);
            return productType;
        }
        public async Task<List<ProductTypeDto>> GetProductTypes(IEnumerable<ProductDto> productDtoList)
        {
            if ((productDtoList == null) || (!productDtoList.Any()))
                throw new InvalidProductException("Invalid ProductDtoList");

            var result = await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProductTypes"]));
            var productTypeList = JsonConvert.DeserializeObject<IEnumerable<ProductTypeDto>>(result.Content.ReadAsStringAsync().Result);
            var filteredProductTypeList = productTypeList.Where(ptdto => productDtoList.Any(pdto => ptdto.Id == pdto.ProductTypeId)).ToList();
            return filteredProductTypeList;
        }
    }
}
