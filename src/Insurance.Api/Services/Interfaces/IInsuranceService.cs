﻿using Insurance.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Services.Interfaces
{
    public interface IInsuranceService
    {
        public List<Insurance.Domain.Entities.Insurance> CalculateInsurance(List<InsuranceDto> insuranceDtoList);

    }
}
