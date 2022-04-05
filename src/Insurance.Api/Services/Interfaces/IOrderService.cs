using Insurance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Api.Services.Interfaces
{
    public interface IOrderService
    {
        public Order CreateOrder(IEnumerable<Insurance.Domain.Entities.Insurance> InsuranceList);
    }
}
