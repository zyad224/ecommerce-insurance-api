using AutoMapper;
using Insurance.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Extensions
{
    public static class AutoMapperExtenstion
    {
        public static List<InsuranceDto> MergeMap( this IMapper mapper, List<ProductDto> productDtoList, List<ProductTypeDto> productTypeDtoList)
        {
            List<InsuranceDto> insuranceDtoList = new List<InsuranceDto>();

            for (int i = 0; i < productDtoList.Count; i++)
            {
                var insuranceDto = mapper.Map<InsuranceDto>(productDtoList[i]);
                mapper.Map<ProductTypeDto, InsuranceDto>(productTypeDtoList[i], insuranceDto);
                insuranceDtoList.Add(insuranceDto);
            }

            return insuranceDtoList;
        }
    }
}
