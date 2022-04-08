using System.Collections.Generic;
using System.Linq;

namespace Insurance.Api.Dtos
{
    public class OrderDto
    {
        public List<InsuranceDto> InsuranceDtoList { get; set; }
        public float OrderInsuranceValue { get; set; }
        public bool IsValid()
        {
            if((InsuranceDtoList == null)||
               (!InsuranceDtoList.Any())) 
                return false;
            return true;
        }
    }
}
