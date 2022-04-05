using Insurance.Domain.Base;
using Insurance.Domain.DomainExceptions;
using Insurance.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Insurance.Domain.Entities
{
    public class Order: BaseEntity
    {
        [Key]
        public string Id { get; private set; }
        public float OrderInsuranceValue { get; private set; }
        public IEnumerable<Insurance> InsuranceList { get; }

        private Order()
        {

        }

        public Order(IEnumerable<Insurance> insuranceList)
        {
            if ((insuranceList == null) || (!insuranceList.Any()))
                throw new InvalidOrderException("Invalid Order Parameters");

            Id = UUIDGenerator.NewUUID();
            InsuranceList = insuranceList;

        }

        public void SetTotalInsuranceValue()
        {    
            if(OrderInsuranceValue == 0)
            {

                foreach (var insurance in InsuranceList)
                {
                    OrderInsuranceValue += insurance.InsuranceValue;
                }

                if(InsuranceList.Any(i=>i.ProductTypeName == "Digital cameras"))
                {
                    OrderInsuranceValue += 500;
                }
            }
          
        }

    }
}
