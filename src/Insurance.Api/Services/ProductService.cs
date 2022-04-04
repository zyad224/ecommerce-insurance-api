using Insurance.Api.Dtos;
using Insurance.Api.Services.Interfaces;
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
        public async Task<ProductTypeDto> GetProductType(ProductDto productDto)
        {         
            var result= await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProductType"], productDto.ProductTypeId));
            var productType = JsonConvert.DeserializeObject<ProductTypeDto>(result.Content.ReadAsStringAsync().Result);
            return productType;
        }

        public async Task<ProductDto> GetProduct(int productID)
        {
            var result = await _httpClient.GetAsync(string.Format(_configuration["ProductApi:GetProduct"], productID));
            var product = JsonConvert.DeserializeObject<ProductDto>(result.Content.ReadAsStringAsync().Result);     
            return product;
        }

     
    }
}
