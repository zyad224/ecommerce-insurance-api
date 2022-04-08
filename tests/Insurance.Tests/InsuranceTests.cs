using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Api.Controllers;
using Insurance.Api.Dtos;
using Insurance.Domain.DomainExceptions;
using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace Insurance.Tests
{
    public class InsuranceTests: IClassFixture<InsuranceFixture>
    {
        private readonly InsuranceFixture _insurancefixture;
        public InsuranceTests(InsuranceFixture insurancefixture)
        {
            _insurancefixture = insurancefixture;
        }
        [Fact]
        [Trait("CalculateInsurance", "Bestcase")]
        /*
         * If the product sales price is less than € 500, no insurance required
         */
        public async Task CalculateInsurance_SalesPriceLessThan500_NoSurcharge_MP3Player_InsuranceEquals_Zero()
        {
            //Arrange
            const float expectedInsuranceValue = 0;
            var insuranceDto = new InsuranceDto { ProductId = 1};
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto};
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService,_insurancefixture._insuranceService,_insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Bestcase")]
        /*
         * If the product sales price=> € 500 but < € 2000, insurance cost is € 1000
         */
        public async Task CalculateInsurance_SalesPriceBetween500and2000_NoSurcharge_MP3Player_InsuranceEquals_1000Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 1000;
            var insuranceDto = new InsuranceDto { ProductId = 3 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService,_insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Bestcase")]
        /*
         * If the product sales price=> € 2000, insurance cost is €2000
         */
        public async Task CalculateInsurance_SalesPriceEquals2000_NoSurcharge_MP3Player_InsuranceEquals_2000Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 2000;
            var insuranceDto = new InsuranceDto { ProductId = 4 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Bestcase")]
        /*
         * If the type of the product is a smartphone or a laptop, add € 500 more to the insurance cost
         */
        public async Task CalculateInsurance_SalesPriceEquals1000_NoSurcharge_SmartPhone_InsuranceEquals_1500Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 1500;
            var insuranceDto = new InsuranceDto { ProductId = 5 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Bestcase")]
        /*
         * If the type of the product is a smartphone or a laptop, add € 500 more to the insurance cost
         */
        public async Task CalculateInsurance_SalesPriceEquals3000_NoSurcharge_Laptop_InsuranceEquals_2500Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 2500;
            var insuranceDto = new InsuranceDto { ProductId = 6 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Bestcase")]
        /*
         * When customers buy a laptop that costs less than € 500, insurance should be € 500.
         */
        public async Task CalculateInsurance_SalesPriceLessThan500_NoSurcharge_Laptop_InsuranceEquals_500Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 500;
            var insuranceDto = new InsuranceDto { ProductId = 7 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Bestcase")]
        /*
         * When customers buy a smartphone that costs less than € 500, insurance should be € 500.
         */
        public async Task CalculateInsurance_SalesPriceLessThan500_NoSurcharge_Smartphone_InsuranceEquals_500Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 500;
            var insuranceDto = new InsuranceDto { ProductId = 8 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Worstcase")]
        public async Task CalculateInsurance_EmptyInsuranceDtoList_Returns_BadRequest()
        {
            //Arrange
            var insuranceDtoList = new List<InsuranceDto>();
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;

            //Assert
            Assert.True(resultResponseStatusCode == 400);
        }
        [Fact]
        [Trait("CalculateInsurance", "Worstcase")]
        public void CalculateInsurance_ProductIdEqualsZero_Throws_InvalidProductException()
        {
            //Arrange
            var insuranceDto1 = new InsuranceDto { ProductId = 0 };
            var insuranceDto2 = new InsuranceDto { ProductId = 1 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto1, insuranceDto2 };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Assert
            Assert.ThrowsAsync<InvalidProductException>(() => insuranceController.CalculateInsurance(orderDto));          
        }
        [Fact]
        [Trait("CalculateInsurance", "Worstcase")]
        public async Task CalculateInsurance_NullOrderDto_Returns_BadRequest()
        {
            //Arrange
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(null);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;

            //Assert
            Assert.True(resultResponseStatusCode == 400);
        }
        [Fact]
        [Trait("CalculateInsurance", "OrderInShoppingCart")]
        public async Task CalculateInsurance_TwoOrdersInShoppingCart_NoSurcharge_SalesPriceLessThan500_Laptops_OrderInsuranceValueEquals_1000Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 1000;
            var insuranceDto1 = new InsuranceDto { ProductId = 7 };
            var insuranceDto2 = new InsuranceDto { ProductId = 9 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto1, insuranceDto2 };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "OrderInShoppingCart")]
        public async Task CalculateInsurance_TwoOrdersInShoppingCart_NoSurcharge_SalesPriceLessThan500_DigitalCameraIncluded_OrderInsuranceValueEquals_500Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 500;
            var insuranceDto1 = new InsuranceDto { ProductId = 2 };
            var insuranceDto2 = new InsuranceDto { ProductId = 1 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto1, insuranceDto2 };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "OrderInShoppingCart")]
        public async Task CalculateInsurance_TwoOrdersInShoppingCart_NoSurcharge_SalesPriceLessThan500_TwoDigitalCameraIncluded_OrderInsuranceValueEquals_500Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 500;
            var insuranceDto1 = new InsuranceDto { ProductId = 2 };
            var insuranceDto2 = new InsuranceDto { ProductId = 10};
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto1, insuranceDto2 };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "OrderInShoppingCart")]
        public async Task CalculateInsurance_OneOrderInShoppingCart_NoSurcharge_SalesPriceEqualsTo3000_CannotBeInsured_OrderInsuranceValueEquals_0Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 0;
            var insuranceDto1 = new InsuranceDto { ProductId = 11 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto1,  };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Edgecase")]
        public async Task CalculateInsurance_TwoOrderInShoppingCartWithEqualProductId_NoSurcharge_SalesPriceEqualsTo2000_Mp3_OrderInsuranceValueEquals_4000Euro()
        {
            //Arrange
            const float expectedInsuranceValue = 4000;
            var insuranceDto1 = new InsuranceDto { ProductId = 4 };
            var insuranceDto2 = new InsuranceDto { ProductId = 4 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto1, insuranceDto2 };
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            Assert.True(resultResponseStatusCode == 200);
            Assert.True(expectedInsuranceValue == orderInsuranceValue);
        }
        [Fact]
        [Trait("CalculateInsurance", "Surcharge")]
        public async Task CalculateInsurance_OneOrderInShoppingCart_SurchargeFeesEqual200_SalesPriceEqualsTo3000_Mp3_IfSurchargeExist_OrderInsuranceValueEquals2200_IfSurchargeNotExist_OrderInsuranceValueEquals2000()
        {
            //Arrange
            const float expectedInsuranceValueIfSurchargeExists = 2200;
            const float expectedInsuranceValueIfSurchargeNotExists = 2000;
            var insuranceDto1 = new InsuranceDto { ProductId = 12 };
            var insuranceDtoList = new List<InsuranceDto>() { insuranceDto1};
            var orderDto = new OrderDto { InsuranceDtoList = insuranceDtoList };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.CalculateInsurance(orderDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;
            var requestResponse = ((result.Result as ObjectResult).Value) as OrderDto;
            var orderInsuranceValue = requestResponse.OrderInsuranceValue;

            //Assert
            if(orderInsuranceValue == expectedInsuranceValueIfSurchargeExists)
            {
                Assert.True(expectedInsuranceValueIfSurchargeExists == orderInsuranceValue);

            }
            else if(orderInsuranceValue == expectedInsuranceValueIfSurchargeNotExists)
            {
                Assert.True(expectedInsuranceValueIfSurchargeNotExists == orderInsuranceValue);

            }
            Assert.True(resultResponseStatusCode == 200);
        }
        [Fact]
        [Trait("UpdateSurcharge", "Bestcase")]
        public async Task Surcharges_UpdateSurcharge_IfSurchargeDontExist_Throws_NotFoundSurchargeException_Else_UpdateSurcharge_Return_NoContent()
        {
            //Arrange
            const int queryId = 15;
            SurchargeDto surchargeDto = new SurchargeDto { ProductTypeId = 15, SurChargeFees = 200 };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            try
            {
                var result = await insuranceController.UpdateSurcharge(queryId, surchargeDto);
                var resultResponseStatusCode = (result.Result as NoContentResult).StatusCode;
                Assert.True(resultResponseStatusCode == 204);

            }
            catch (Exception e)
            {
                Assert.True(e.GetType() == typeof(NotFoundSurchargeException));
            }

        }
        [Fact]
        [Trait("UploadSurcharges", "Bestcase")]
        public async Task Surcharges_Upload_TwoSurchargesForTwoProductTypes_Return_Created()
        {
            //Arrange
            SurchargeDto surchargeDto1 = new SurchargeDto { ProductTypeId = 15, SurChargeFees = 100 };
            SurchargeDto surchargeDto2 = new SurchargeDto { ProductTypeId = 16, SurChargeFees = 200 };
            List<SurchargeDto> surchargeDtoReq = new List<SurchargeDto>() { surchargeDto1, surchargeDto2 };          
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.UploadSurcharges(surchargeDtoReq);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;

            //Assert
            Assert.True(resultResponseStatusCode == 201);
        }
        [Fact]
        [Trait("UploadSurcharges", "Worstcase")]
        public async Task Surcharges_Upload_EmptySurchargesList_Return_BadRequest()
        {
            //Arrange
            List<SurchargeDto> surchargeDtoReq = new List<SurchargeDto>();
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.UploadSurcharges(surchargeDtoReq);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;

            //Assert
            Assert.True(resultResponseStatusCode == 400);
        }
        [Fact]
        [Trait("UploadSurcharges", "Worstcase")]
        public async Task Surcharges_Upload_Null_Return_BadRequest()
        {
            //Arrange
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.UploadSurcharges(null);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;

            //Assert
            Assert.True(resultResponseStatusCode == 400);
        }
        [Fact]
        [Trait("UploadSurcharges", "Worstcase")]
        public void Surcharges_Upload_SurchargeWithProductTypeIdEqualsZero_Throws_InvalidSurchargeException()
        {
            //Arrange
            SurchargeDto surchargeDto1 = new SurchargeDto { ProductTypeId = 0, SurChargeFees = 100 };
            SurchargeDto surchargeDto2 = new SurchargeDto { ProductTypeId = 15, SurChargeFees = 200 };
            List<SurchargeDto> surchargeDtoReq = new List<SurchargeDto>() { surchargeDto1, surchargeDto2 };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Assert
            Assert.ThrowsAsync<InvalidSurchargeException>(() => insuranceController.UploadSurcharges(surchargeDtoReq));    
        }
        [Fact]
        [Trait("UpdateSurcharge", "Worstcase")]
        public async Task Surcharges_UpdateSurcharge_QueryIdNotEqualToModelId_Return_BadRequest()
        {
            //Arrange
            const int queryId = 1;
            SurchargeDto surchargeDto = new SurchargeDto { ProductTypeId = 15, SurChargeFees = 100 };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.UpdateSurcharge(queryId,surchargeDto);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;

            //Assert
            Assert.True(resultResponseStatusCode == 400);
        }
        [Fact]
        [Trait("UpdateSurcharge", "Worstcase")]
        public async Task Surcharges_UpdateSurcharge_NullSurchargeDto_Return_BadRequest()
        {
            //Arrange
            const int queryId = 1;
            SurchargeDto surchargeDto = new SurchargeDto { ProductTypeId = 15, SurChargeFees = 100 };
            var insuranceController = new InsuranceController(_insurancefixture._productService, _insurancefixture._insuranceService, _insurancefixture._mapper);

            //Act
            var result = await insuranceController.UpdateSurcharge(queryId, null);
            var resultResponseStatusCode = (result.Result as ObjectResult).StatusCode;

            //Assert
            Assert.True(resultResponseStatusCode == 400);
        }
    }
}