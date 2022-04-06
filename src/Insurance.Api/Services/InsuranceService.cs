using Insurance.Api.Dtos;
using Insurance.Api.Services.Interfaces;
using Insurance.Domain.DomainExceptions;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Insurance.Api.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public InsuranceService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public async Task<List<Insurance.Domain.Entities.Insurance>> CalculateInsurance(List<InsuranceDto> insuranceDtoList)
        {
            if ((insuranceDtoList == null) || (!insuranceDtoList.Any()))
                throw new InvalidInsuranceException("InsuranceDtoList is NULL/Empty");

            List<Insurance.Domain.Entities.Insurance> insuranceList = new List<Domain.Entities.Insurance>();
            foreach (var insuranceDto in insuranceDtoList)
            {
                var surCharge = await _unitOfWork.SurchargeRepo.GetSurchargeByProductTypeId(insuranceDto.ProductTypeId);
                var insurance = new Insurance.Domain.Entities.Insurance(insuranceDto.ProductId,insuranceDto.ProductTypeId, insuranceDto.ProductTypeName, insuranceDto.SalesPrice,insuranceDto.ProductTypeHasInsurance);

                if (surCharge != null)
                {
                    insurance.SetIsSurCharge(true);
                    insurance.SetSurChargeFees(surCharge.SurChargeFees);
                }
                insurance.SetInsuranceValue();
                insuranceList.Add(insurance);
            }

            return insuranceList;       
        }
        public float TotalInsurance(IEnumerable<Insurance.Domain.Entities.Insurance> insuranceList, float extraInsuranceFees)
        {

            if ((insuranceList == null) || (!insuranceList.Any()))
                throw new InvalidInsuranceException("InsuranceList is NULL/Empty");

            float totalInsurance = 0;

            foreach (var insurance in insuranceList)
            {
                totalInsurance += insurance.InsuranceValue;
            }

            totalInsurance += extraInsuranceFees;
            return totalInsurance;
        }
        public float ExtraInsuranceFees(IEnumerable<Insurance.Domain.Entities.Insurance> insuranceList)
        {
            if ((insuranceList == null) || (!insuranceList.Any()))
                throw new InvalidInsuranceException("InsuranceList is NULL/Empty");

            float extraInsuranceFees = 0;

            if (insuranceList.Any(i => i.ProductTypeName == "Digital cameras"))
            {
                extraInsuranceFees += 500;
            }

            return extraInsuranceFees;

        }
        public async Task AddSurcharge(List<Surcharge> surChargeDtoList)
        {
            if ((surChargeDtoList == null) || (!surChargeDtoList.Any()))
                throw new InvalidSurchargeException("SurChargeDto List is NULL/Empty");

            foreach (var surcharge in surChargeDtoList)
            {
               await  _unitOfWork.SurchargeRepo.Add(surcharge);
            }

            await _unitOfWork.Commit();
        }
        public async Task UpdateSurcharge(SurchargeDto surChargeDto)
        {
            if ((surChargeDto == null) ||
                (surChargeDto.ProductTypeId == 0))
                throw new InvalidSurchargeException("SurChargeDto is NULL/Empty");

            var surCharge = await _unitOfWork.SurchargeRepo.GetSurchargeByProductTypeId(surChargeDto.ProductTypeId);

            if (surCharge == null)
                throw new NotFoundSurchargeException("Not Found Surcharge");

            surCharge.SetSurChargeFees(surChargeDto.SurChargeFees);
            surCharge.SetProductTypeId(surChargeDto.ProductTypeId);

            _unitOfWork.SurchargeRepo.Update(surCharge);
            await _unitOfWork.Commit();

        }

    }
}
