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
        /// /// <response code="200"> OrderDto: It consists of List of InsuranceDto and OrderInsuranceValue </response>
        /// <response code="400">Invalid Products API Endpoint Model</response>        
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
            var extraInsuranceFees = _insuranceService.ExtraInsuranceFees(insuranceList);
            var totalInsurance = _insuranceService.TotalInsurance(insuranceList,extraInsuranceFees,orderDtoReq.InsuranceDtoList);
            _mapper.Map<List<Insurance.Domain.Entities.Insurance>, List<InsuranceDto>>(insuranceList, insuranceDtoList);
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList, OrderInsuranceValue = totalInsurance };         
            return Ok(orderDto);
        }
        /// <summary>
        /// Surcharges endpoint is responsible to upload Surcharges per Product Type.
        /// The endpoint recieves List of SurchargeDto and save them in the database.
        /// </summary>
        /// /// <response code="201"> Creates a new list of Surcharges in the database</response>
        /// <response code="400">Invalid Surcharges API Endpoint Model</response>   
        [HttpPost]
        [Route("surcharges")]
        public async Task<ActionResult<List<SurchargeDto>>> UploadSurcharges([FromBody] List<SurchargeDto> surchargeDtoReq)
        {
            if ((surchargeDtoReq == null) || (!surchargeDtoReq.Any()))
                return BadRequest("Invalid /surcharges API Endpoint Model");
            var surChargeList = _mapper.Map<List<Insurance.Domain.Entities.Surcharge>>(surchargeDtoReq);
            await _insuranceService.AddSurcharge(surChargeList);
            return CreatedAtAction("Created", surchargeDtoReq);
        }
        /// <summary>
        /// Surcharges endpoint is responsible to update an already existing surcharge.
        /// The endpoint recieves SurchargeDto that has ProductTypeId and SurchargesFees.
        /// The endpoint uses the ProductTypeId and updates its corresponding Surcharge.
        /// </summary>
        /// /// <response code="204"> Updates an already existing Surcharge</response>
        /// <response code="400">Invalid Surcharges API Endpoint Model</response>   
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