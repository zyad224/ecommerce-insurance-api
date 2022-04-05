using Insurance.Api.Dtos;
using Insurance.Api.Services.Interfaces;
using Insurance.Domain.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Services
{
    public class InsuranceService : IInsuranceService
    {
        public List<Insurance.Domain.Entities.Insurance> CalculateInsurance(List<InsuranceDto> insuranceDtoList)
        {
            if ((insuranceDtoList == null) || (insuranceDtoList.Count == 0))
                throw new InvalidInsuranceException("InsuranceDtoList is NULL/Empty");

            List<Insurance.Domain.Entities.Insurance> insuranceList = new List<Domain.Entities.Insurance>();

            foreach (var insuranceDto in insuranceDtoList)
            {
                var insurance = new Insurance.Domain.Entities.Insurance(insuranceDto.ProductId, insuranceDto.ProductTypeName, insuranceDto.SalesPrice,insuranceDto.ProductTypeHasInsurance);
                insurance.SetInsuranceValue();
                insuranceList.Add(insurance);
            }

            return insuranceList;       
        }

     
    }
}
