using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api.Dtos;
using Insurance.Api.Extensions;
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
        [Route("products")]
        public async Task<ActionResult<InsuranceDto>> CalculateInsurance([FromBody] InsuranceDto insuranceDtoReq)
        {
            if ((insuranceDtoReq == null) || (insuranceDtoReq.ProductId == 0))
                return BadRequest("Invalid API Model");

            var productDto = await _productService.GetProduct(insuranceDtoReq.ProductId);
            var productTypeDto = await _productService.GetProductType(productDto);

            var insuranceDto = _mapper.Map<InsuranceDto>(productDto);
            _mapper.Map<ProductTypeDto, InsuranceDto>(productTypeDto, insuranceDto);
            var insuranceDtoList = new List<InsuranceDto> { insuranceDto };
            
            var insuranceList = await _insuranceService.CalculateInsurance(insuranceDtoList); 
            _mapper.Map<Insurance.Domain.Entities.Insurance, InsuranceDto>(insuranceList.FirstOrDefault(), insuranceDto);
         
            return Ok(insuranceDto);
        }
        [HttpPost]
        [Route("orders")]
        public async Task<ActionResult<OrderDto>> CalculateInsurance([FromBody] OrderDto orderDtoReq)
        {
            if ((orderDtoReq == null) || (!orderDtoReq.InsuranceDtoList.Any()))
                return BadRequest("Invalid API Model");

            var productDtoList = await _productService.GetProducts(orderDtoReq.InsuranceDtoList.Select(idto => idto.ProductId));
            var productTypeDtoList = await _productService.GetProductTypes(productDtoList);
            var insuranceDtoList = productDtoList.Merge(productTypeDtoList);

            var insuranceList = await _insuranceService.CalculateInsurance(insuranceDtoList);
            var extraInsuranceFees = _insuranceService.ExtraInsuranceFees(insuranceList);
            var totalInsurance = _insuranceService.TotalInsurance(insuranceList,extraInsuranceFees);
            _mapper.Map<List<Insurance.Domain.Entities.Insurance>, List<InsuranceDto>>(insuranceList, insuranceDtoList);

            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList, OrderInsuranceValue = totalInsurance };
                   
            return Ok(orderDto);
        }
        [HttpPost]
        [Route("surcharges")]
        public async Task<ActionResult<List<SurchargeDto>>> Surcharges([FromBody] List<SurchargeDto> surchargeDtoReq)
        {
            if ((surchargeDtoReq == null) || (!surchargeDtoReq.Any()))
                return BadRequest("Invalid API Model");

            var surChargeList = _mapper.Map<List<Insurance.Domain.Entities.Surcharge>>(surchargeDtoReq);
            await _insuranceService.AddSurcharge(surChargeList);
            return Ok();
        }
        [HttpPut]
        [Route("surcharges/{productTypeId}")]
        public async Task<ActionResult<List<SurchargeDto>>> Surcharges(int productTypeId, [FromBody] SurchargeDto surchargeDtoReq)
        {
            if ((surchargeDtoReq == null) || (productTypeId == 0))
                return BadRequest("Invalid API Model");

            await _insuranceService.UpdateSurcharge(surchargeDtoReq);
            return Ok();
        }

    }
}