using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api.Dtos;
using Insurance.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers
{
    [Route("api/insurance")]
    [ApiController]
    public class InsuranceController: Controller
    {

        private readonly IProductService _productService;
        private readonly IInsuranceService _insuranceService;
        private readonly IMapper _mapper;

        public InsuranceController(IProductService productService, IInsuranceService insuranceService,IMapper mapper)
        {
            _productService = productService;
            _insuranceService = insuranceService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("product")]
        public async Task<ActionResult<InsuranceDto>> CalculateInsurance([FromBody] InsuranceDto insuranceDtoReq)
        {
            var productDto = await _productService.GetProduct(insuranceDtoReq.ProductId);
            var productTypeDto = await _productService.GetProductType(productDto);

            var insuranceDto = _mapper.Map<InsuranceDto>(productDto);
            _mapper.Map<ProductTypeDto, InsuranceDto>(productTypeDto, insuranceDto);

            var insurance = _insuranceService.CalculateInsurance(insuranceDto);
            insuranceDto.InsuranceValue = insurance.InsuranceValue;
            return insuranceDto;
        }

    }
}