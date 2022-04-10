using Insurance.Domain.Entities;
using System.Threading.Tasks;
namespace Insurance.Domain.Interfaces
{
    public interface ISurchargeRepository
    {
        public Task AddSurcharge (Surcharge surCharge);
        public Task<Surcharge> GetSurchargeByProductTypeId(int productTypeId);
        public void UpdateSurcharge(Surcharge surCharge);
    }
}
