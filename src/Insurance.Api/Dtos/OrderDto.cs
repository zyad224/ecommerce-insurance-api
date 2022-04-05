using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Dtos
{
    public class OrderDto
    {

        public List<InsuranceDto> InsuranceDtoList { get; set; }
        public float OrderInsuranceValue { get; set; }

    }
}
