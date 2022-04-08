using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Insurance.Api.Dtos
{
    public class InsuranceDto
    {
        [Required(ErrorMessage = "ProductId is Required")]
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public float InsuranceValue { get; set; }
        [JsonIgnore]
        public string ProductTypeName { get; set; }
        [JsonIgnore]
        public bool ProductTypeHasInsurance { get; set; }
        [JsonIgnore]
        public float SalesPrice { get; set; }
    }
}
