using Insurance.Api.Dtos;
using Insurance.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Insurance.Api.Services.Interfaces
{
    public interface IInsuranceService
    {
        public Task<List<Insurance.Domain.Entities.Insurance>> CalculateInsurance(IEnumerable<InsuranceDto> insuranceDtoList);
        public float TotalInsurance(IEnumerable<Insurance.Domain.Entities.Insurance> InsuranceList,IEnumerable<InsuranceDto> insuranceDtoList,float extraInsuranceFees);
        public float ExtraInsuranceFees(IEnumerable<InsuranceDto> insuranceDtoList);
        public Task AddSurcharge(IEnumerable<Surcharge> surChargeDtoList);
        public Task UpdateSurcharge(SurchargeDto surChargeDto);
    }
}
