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
        /// <summary>
        /// Products endpoint is responsible to calculate overall insurance for a list of products.
        /// The endpoint recieves OrderDto and calculates the insurance for each product in the list.
        /// </summary>
        /// <returns>The endpoint returns the list of InsuranceDto and the overall OrderInsuranceValue.</returns>
        /// /// <response code="200"> Calculates Insurance for each product in InsuranceDtoList and Calculates the overall OrderInsuranceValue</response>
        /// <response code="400">OrderDto submitted is NULL or the InsuranceDtoList is NULL/Empty</response>        
        [HttpPost]
        [Route("products")]
        public async Task<ActionResult<OrderDto>> CalculateInsurance([FromBody] OrderDto orderDtoReq)
        {
            if ((orderDtoReq == null) || (!orderDtoReq.IsValid()))
                return BadRequest("Invalid /products API Endpoint Model");
            var productDtoList = await _productService.GetProducts(orderDtoReq.InsuranceDtoList.Select(idto => idto.ProductId));
            var productTypeDtoList = await _productService.GetProductTypes(productDtoList);
            var insuranceDtoList = productDtoList.Merge(productTypeDtoList);
            var insuranceList = await _insuranceService.CalculateInsurance(insuranceDtoList);
            var extraInsuranceFees = _insuranceService.ExtraInsuranceFees(insuranceDtoList);
            var totalInsurance = _insuranceService.TotalInsurance(insuranceList,orderDtoReq.InsuranceDtoList,extraInsuranceFees);
            _mapper.Map<List<Insurance.Domain.Entities.Insurance>, List<InsuranceDto>>(insuranceList, insuranceDtoList);
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList, OrderInsuranceValue = totalInsurance };         
            return Ok(orderDto);
        }
        /// <summary>
        /// Surcharges endpoint is responsible to upload Surcharges per Product Type.
        /// The endpoint recieves List of SurchargeDto and save them in the database.
        /// </summary>
        /// /// <response code="201"> Creates a new list of Surcharges in the database</response>
        /// <response code="400">List of Surcharges submitted is NULL or Empty</response>
        /// <response code="409">List of Surcharges submitted Already Exists in the System</response> 
        [HttpPost]
        [Route("surcharges")]
        public async Task<ActionResult<List<SurchargeDto>>> UploadSurcharges([FromBody] List<SurchargeDto> surchargeDtoReq)
        {
            if ((surchargeDtoReq == null) || (!surchargeDtoReq.Any()))
                return BadRequest("Invalid /surcharges API Endpoint Model");
            var surChargeList = _mapper.Map<List<Insurance.Domain.Entities.Surcharge>>(surchargeDtoReq);
            await _insuranceService.AddSurcharge(surChargeList);
            return CreatedAtAction("UploadSurcharges", surchargeDtoReq);
        }
        /// <summary>
        /// Surcharges endpoint is responsible to update an already existing surcharge.
        /// The endpoint recieves SurchargeDto that has ProductTypeId and SurchargesFees.
        /// The endpoint uses the ProductTypeId and updates its corresponding Surcharge.
        /// </summary>
        /// /// <response code="204"> Updates an already existing Surcharge</response>
        /// <response code="400">ShurchargeDto submitted is NULL or query id != SurchargeDto.ProductTypeId</response> 
        /// <response code="404">Shurcharge with ProductTypeId submitted does not exist in database</response>
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