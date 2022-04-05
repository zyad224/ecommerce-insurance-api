using Insurance.Domain.Base;
using Insurance.Domain.DomainExceptions;
using Insurance.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Insurance.Domain.Entities
{
    public class Insurance: BaseEntity
    {
        [Key]
        public string Id { get; private set; }
        public int ProductId { get; private set; }
        public string ProductTypeName { get; private set; }
        public float InsuranceValue { get; private set; }
        public float SalesPrice { get; private set; }
        public bool ProductTypeHasInsurance { get; private set; }


        private Insurance()
        {

        }

        public Insurance(int productId,string productTypeName, float salesPrice, bool productTypeHasInsurance)
        {
            if ((productId == 0) || (string.IsNullOrEmpty(productTypeName)) || (salesPrice == 0))
                throw new InvalidInsuranceException("Invalid Insurance Parameters");

            
            Id = UUIDGenerator.NewUUID();
            ProductId = productId;
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


                if ((this.ProductTypeName == "Laptops" ||
                          this.ProductTypeName == "Smartphones"))
                    this.InsuranceValue += 500;

                ModifiedOn = DateTime.UtcNow;
            }

        }
    }
}
