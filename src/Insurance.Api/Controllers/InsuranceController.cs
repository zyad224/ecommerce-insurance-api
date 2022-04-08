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
        public async Task<ActionResult<OrderDto>> CalculateInsurance([FromBody] OrderDto orderDtoReq)
        {
            if ((orderDtoReq == null) || (!orderDtoReq.IsValid()))
                return BadRequest("Invalid /products API Model");

            var productDtoList = await _productService.GetProducts(orderDtoReq.InsuranceDtoList.Select(idto => idto.ProductId));
            var productTypeDtoList = await _productService.GetProductTypes(productDtoList);
            var insuranceDtoList = productDtoList.Merge(productTypeDtoList);

            var insuranceList = await _insuranceService.CalculateInsurance(insuranceDtoList);
            var extraInsuranceFees = _insuranceService.ExtraInsuranceFees(insuranceList);
            var totalInsurance = _insuranceService.TotalInsurance(insuranceList,extraInsuranceFees,orderDtoReq.InsuranceDtoList);
            _mapper.Map<List<Insurance.Domain.Entities.Insurance>, List<InsuranceDto>>(insuranceList, insuranceDtoList);

            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList, OrderInsuranceValue = totalInsurance };         
            return Ok(orderDto);
        }
        [HttpPost]
        [Route("surcharges")]
        public async Task<ActionResult<List<SurchargeDto>>> UploadSurcharges([FromBody] List<SurchargeDto> surchargeDtoReq)
        {
            if ((surchargeDtoReq == null) || (!surchargeDtoReq.Any()))
                return BadRequest("Invalid /surcharges API Model");

            var surChargeList = _mapper.Map<List<Insurance.Domain.Entities.Surcharge>>(surchargeDtoReq);
            await _insuranceService.AddSurcharge(surChargeList);
            return CreatedAtAction("Created", surchargeDtoReq);
        }
        [HttpPut]
        [Route("surcharges/{id}")]
        public async Task<ActionResult<List<SurchargeDto>>> UpdateSurcharge(int id, [FromBody] SurchargeDto surchargeDtoReq)
        {
            if ((surchargeDtoReq == null) || (id != surchargeDtoReq.ProductTypeId))
                return BadRequest("Invalid /surcharges/{id} API Model");

            await _insuranceService.UpdateSurcharge(surchargeDtoReq);
            return NoContent();
        }
    }
}