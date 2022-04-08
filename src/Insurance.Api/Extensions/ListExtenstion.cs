using Insurance.Api.Dtos;
using System;
using System.Collections.Generic;
namespace Insurance.Api.Extensions
{
    public static class ListExtenstion
    {
        public static List<InsuranceDto> Merge( this List<ProductDto> productDtoList, List<ProductTypeDto> productTypeDtoList)
        {
            if ((productDtoList == null) || (productTypeDtoList == null))
                throw new ArgumentNullException("ProductDtoList or ProductTypeDtoList is NULL");

            List<InsuranceDto> insuranceDtoList = new List<InsuranceDto>();
            foreach (var productDto in productDtoList)
            {
                foreach (var productType in productTypeDtoList)
                {
                    if(productDto?.ProductTypeId == productType?.Id)
                    {
                        var insuranceDto = new InsuranceDto
                        {
                            ProductId = productDto.Id,
                            ProductTypeId = productType.Id,
                            ProductTypeName = productType.Name,
                            ProductTypeHasInsurance = productType.CanBeInsured,
                            SalesPrice = productDto.SalesPrice
                        };

                        insuranceDtoList.Add(insuranceDto);
                    }
                }
            }

            return insuranceDtoList;
        }
    }
}
