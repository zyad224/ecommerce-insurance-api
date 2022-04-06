using Insurance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Interfaces
{
    public interface ISurchargeRepository
    {
        public Task<IEnumerable<Surcharge>> GetAll();
        public Task Add (Surcharge surCharge);
        public Task<Surcharge> GetSurchargeByProductTypeId(int productTypeId);
        public void Update(Surcharge surCharge);
    }
}
