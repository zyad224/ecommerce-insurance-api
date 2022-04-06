using Insurance.Api.Dtos;
using Insurance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Services.Interfaces
{
    public interface IInsuranceService
    {
        public Task<List<Insurance.Domain.Entities.Insurance>> CalculateInsurance(List<InsuranceDto> insuranceDtoList);
        public float TotalInsurance(IEnumerable<Insurance.Domain.Entities.Insurance> InsuranceList, float extraInsuranceFees);
        public float ExtraInsuranceFees(IEnumerable<Insurance.Domain.Entities.Insurance> insuranceList);
        public Task AddSurcharge(List<Surcharge> surChargeDtoList);
        public Task UpdateSurcharge(SurchargeDto surChargeDto);
    }
}
