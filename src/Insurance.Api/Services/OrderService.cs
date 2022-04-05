using Insurance.Api.Services.Interfaces;
using Insurance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insurance.Domain.DomainExceptions;

namespace Insurance.Api.Services
{
    public class OrderService : IOrderService
    {
        public Order CreateOrder(IEnumerable<Domain.Entities.Insurance> InsuranceList)
        {           
            var order = new Order(InsuranceList);
            order.SetTotalInsuranceValue();
            return order;
        }
    }
}
