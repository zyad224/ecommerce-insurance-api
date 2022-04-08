using Insurance.Domain.Base;
using Insurance.Domain.DomainExceptions;
using Insurance.Domain.Shared;
using System;
namespace Insurance.Domain.Entities
{
    public class Insurance: BaseEntity
    {
        public string Id { get; private set; }
        public int ProductId { get; private set; }
        public int ProductTypeId { get; private set; }
        public string ProductTypeName { get; private set; }
        public float InsuranceValue { get; private set; }
        public float SalesPrice { get; private set; }
        public bool ProductTypeHasInsurance { get; private set; }
        public bool IsSurCharge { get; private set; }
        public float SurChargeFees { get; private set; }
        private Insurance()
        {

        }
        public Insurance(int productId,int productTypeId,string productTypeName, float salesPrice, bool productTypeHasInsurance)
        {
            if ((productId == 0) ||(productTypeId == 0) || (string.IsNullOrEmpty(productTypeName)) || (salesPrice == 0))
                throw new InvalidInsuranceException("Invalid Insurance Domain Parameters");
       
            Id = UUIDGenerator.NewUUID();
            ProductId = productId;
            ProductTypeId = productTypeId;
            ProductTypeName = productTypeName;
            SalesPrice = salesPrice;
            ProductTypeHasInsurance = productTypeHasInsurance;
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;                      
        }
        public void SetInsuranceValue()
        {
            if(this.ProductTypeHasInsurance)
            {
                if (this.SalesPrice < 500)
                    this.InsuranceValue = 0;

                else if (this.SalesPrice >= 500 && this.SalesPrice < 2000)
                    this.InsuranceValue = 1000;

                else if (this.SalesPrice >= 2000)
                    this.InsuranceValue = 2000;

                if ((this.ProductTypeName == ProductTypeEnum.Laptops.GetString() ||
                     this.ProductTypeName == ProductTypeEnum.Smartphones.GetString()))
                     this.InsuranceValue += 500;

                if (this.IsSurCharge)
                    this.InsuranceValue += this.SurChargeFees;

                ModifiedOn = DateTime.UtcNow;
            }

        }
        public void SetIsSurCharge(bool isSurCharge)
        {
            this.IsSurCharge = isSurCharge;
            ModifiedOn = DateTime.UtcNow;
        }
        public void SetSurChargeFees(float surChargeFees)
        {
            this.SurChargeFees = surChargeFees;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
