using System.ComponentModel.DataAnnotations;
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
