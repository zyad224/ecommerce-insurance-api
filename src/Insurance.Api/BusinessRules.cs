using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Insurance.Api.Controllers;
using Newtonsoft.Json;
using Insurance.Api.Dtos;
using System.Text.Json;

namespace Insurance.Api
{
    public static class BusinessRules
    {
        public static ProductTypeDto GetProductType(string baseAddress, int productID)
        {
           
            HttpClient client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            var json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
        
            var product = JsonConvert.DeserializeObject<ProductDto>(json);

            int productTypeId = product.ProductTypeId;

            json = client.GetAsync(string.Format("/product_types/{0:G}", productTypeId)).Result.Content.ReadAsStringAsync().Result;
            var productType = JsonConvert.DeserializeObject<ProductTypeDto>(json);

            return productType;
        }

        public static float GetSalesPrice(string baseAddress, int productID)
        {
            HttpClient client = new HttpClient{ BaseAddress = new Uri(baseAddress)};
            string json = client.GetAsync(string.Format("/products/{0:G}", productID)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<ProductDto>(json);

            return product.SalesPrice;
        }
    }
}