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
        public Insurance.Domain.Entities.Insurance CalculateInsurance(InsuranceDto insuranceDto)
        {
            if (insuranceDto == null)
                throw new InvalidInsuranceException("InsuranceDto is NULL");
            
            var insurance = new Insurance.Domain.Entities.Insurance(insuranceDto.ProductId, insuranceDto.ProductTypeName, insuranceDto.SalesPrice);
            insurance.SetInsuranceValue();
            return insurance;       
        }
    }
}
