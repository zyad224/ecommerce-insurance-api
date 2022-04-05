using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
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
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public InsuranceController(IProductService productService, IInsuranceService insuranceService, IOrderService orderService,IMapper mapper)
        {
            _productService = productService;
            _insuranceService = insuranceService;
            _orderService = orderService;
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

            var insuranceDtoList = new List<InsuranceDto> { insuranceDto };   
            var insuranceList = _insuranceService.CalculateInsurance(insuranceDtoList);

            _mapper.Map<Insurance.Domain.Entities.Insurance, InsuranceDto>(insuranceList.FirstOrDefault(), insuranceDto);
         
            return insuranceDto;
        }

        [HttpPost]
        [Route("order")]
        public async Task<ActionResult<OrderDto>> CalculateInsurance([FromBody] OrderDto orderDtoReq)
        {
            var productDtoList = await _productService.GetProducts(orderDtoReq.InsuranceDtoList.Select(idto => idto.ProductId));
            var productTypeDtoList = await _productService.GetProductTypes(productDtoList);
            var insuranceDtoList = productDtoList.Merge(productTypeDtoList);

            var insuranceList = _insuranceService.CalculateInsurance(insuranceDtoList);
            var order = _orderService.CreateOrder(insuranceList);
            _mapper.Map<List<Insurance.Domain.Entities.Insurance>, List<InsuranceDto>>(insuranceList, insuranceDtoList);

            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList, OrderInsuranceValue = order.OrderInsuranceValue };
                   
            return orderDto;
        }

    }
}