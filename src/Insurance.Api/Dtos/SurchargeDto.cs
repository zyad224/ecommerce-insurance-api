using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Dtos
{
    public class SurchargeDto
    {
        [Required(ErrorMessage = "SurChargeFees is Required")]
        public float SurChargeFees { get;  set; }
        [Required(ErrorMessage = "ProductTypeId is Required")]
        public int ProductTypeId { get;  set; }
    }
}
