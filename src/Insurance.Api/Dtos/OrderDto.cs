using System.Collections.Generic;
namespace Insurance.Api.Dtos
{
    public class OrderDto
    {
        public List<InsuranceDto> InsuranceDtoList { get; set; }
        public float OrderInsuranceValue { get; set; }
    }
}
