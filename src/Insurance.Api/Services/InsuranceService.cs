using Insurance.Api.Dtos;
using Insurance.Api.Services.Interfaces;
using Insurance.Domain.DomainExceptions;
using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Shared;
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
            if ((insuranceDtoList == null) ||
                (!insuranceDtoList.Any()) || 
                (insuranceDtoList.Any(i=>i.ProductId == 0)) || 
                (insuranceDtoList.Any(i=>i.ProductTypeId == 0)))
                throw new InvalidInsuranceException("Invalid CaluclateInsurance Parameters");

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
        public float ExtraInsuranceFees(IEnumerable<Insurance.Domain.Entities.Insurance> insuranceList)
        {
            if ((insuranceList == null) || (!insuranceList.Any()))
                throw new InvalidInsuranceException("Invalid ExtraInsuranceFees Parameters");

            float extraInsuranceFees = 0;
            if (insuranceList.Any(i => i.ProductTypeName == ProductTypeEnum.Digitalcameras.GetString()))
            {
                extraInsuranceFees += 500;
            }
            return extraInsuranceFees;
        }
        public float TotalInsurance(IEnumerable<Insurance.Domain.Entities.Insurance> insuranceList, float extraInsuranceFees, List<InsuranceDto> insuranceDtoList)
        {
            if ((insuranceList == null) ||
                (!insuranceList.Any()) || 
                (insuranceDtoList == null) || 
                (!insuranceDtoList.Any()) ||
                (insuranceList.Any(i => i.ProductId == 0))||
                (insuranceList.Any(i => i.ProductTypeId == 0))||
                (insuranceDtoList.Any(idto => idto.ProductId == 0)))
                throw new InvalidInsuranceException("Invalid TotalInsurance Parameters");

            float totalInsurance = 0;
            foreach (var insuranceDto in insuranceDtoList)
            {
                if(insuranceList.Any(insurance => insurance.ProductId == insuranceDto.ProductId))
                {
                    var insurance = insuranceList.FirstOrDefault(insurance => insurance.ProductId == insuranceDto.ProductId);
                    totalInsurance += insurance.InsuranceValue;
                }

            }
            totalInsurance += extraInsuranceFees;       
            return totalInsurance;
        }
        public async Task AddSurcharge(List<Surcharge> surChargeDtoList)
        {
            if ((surChargeDtoList == null) ||
                (!surChargeDtoList.Any()) || 
                (surChargeDtoList.Any(s=>s.ProductTypeId == 0)))
                throw new InvalidSurchargeException("Invalid AddSurcharge Parameters");

            foreach (var surcharge in surChargeDtoList)
            {
                var surchargeDb = await _unitOfWork.SurchargeRepo.GetSurchargeByProductTypeId(surcharge.ProductTypeId);
                if (surchargeDb != null)
                    throw new InvalidSurchargeException($"Surcharge Already Exists for ProductTypeId = {surchargeDb.ProductTypeId}");
               await  _unitOfWork.SurchargeRepo.Add(surcharge);
            }
            await _unitOfWork.Commit();
        }
        public async Task UpdateSurcharge(SurchargeDto surChargeDto)
        {
            if ((surChargeDto == null) ||
                (surChargeDto.ProductTypeId == 0))
                throw new InvalidSurchargeException("Invalid UpdateSurcharge Parameters");

            var surCharge = await _unitOfWork.SurchargeRepo.GetSurchargeByProductTypeId(surChargeDto.ProductTypeId);
            if (surCharge == null)
                throw new NotFoundSurchargeException($"Surcharge with ProductTypeId {surChargeDto.ProductTypeId} Not Found");

            surCharge.SetSurChargeFees(surChargeDto.SurChargeFees);
            _unitOfWork.SurchargeRepo.Update(surCharge);
            await _unitOfWork.Commit();
        }
    }
}
