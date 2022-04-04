using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Insurance.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [Route("api/insurance")]
    [ApiController]
    public class InsuranceController: Controller
    {

        private readonly IProductService;
        [HttpPost]
        [Route("/product")]
        public async Task<ActionResult<InsuranceDto>> CalculateInsurance([FromBody] InsuranceDto toInsure)
        {
            int productId = toInsure.ProductId;

            BusinessRules.GetProductType(ProductApi, productId);
            BusinessRules.GetSalesPrice(ProductApi, productId, ref toInsure);

            float insurance = 0f;

            if (toInsure.SalesPrice < 500)
                toInsure.InsuranceValue = 500;
            else
            {
                if (toInsure.SalesPrice > 500 && toInsure.SalesPrice < 2000)
                    if (toInsure.ProductTypeHasInsurance)
                        toInsure.InsuranceValue += 1000;
                if (toInsure.SalesPrice >= 2000)
                    if (toInsure.ProductTypeHasInsurance)
                        toInsure.InsuranceValue += 2000;
                if (toInsure.ProductTypeName == "Laptops" || toInsure.ProductTypeName == "Smartphones" && toInsure.ProductTypeHasInsurance)
                    toInsure.InsuranceValue += 500;
            }

            return toInsure;
        }

        public class InsuranceDto
        {
            public int ProductId { get; set; }
            public float InsuranceValue { get; set; }
            [JsonIgnore]
            public string ProductTypeName { get; set; }
            [JsonIgnore]
            public bool ProductTypeHasInsurance { get; set; }
            [JsonIgnore]
            public float SalesPrice { get; set; }
        }

        private const string ProductApi = "http://localhost:5002";
    }
}