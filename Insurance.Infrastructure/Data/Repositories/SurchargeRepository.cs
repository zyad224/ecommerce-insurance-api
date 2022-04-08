using Insurance.Domain.Entities;
using Insurance.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Insurance.Infrastructure.Data.Repositories
{
    public class SurchargeRepository : ISurchargeRepository
    {
        private readonly IDbApiContext _dbApiContext;

        public SurchargeRepository(IDbApiContext dbApiContext)
        {
            _dbApiContext = dbApiContext;
        }
        public async Task Add(Surcharge surCharge)
        {
            await _dbApiContext.Surcharges.AddAsync(surCharge);
        }
        public async Task<Surcharge> GetSurchargeByProductTypeId(int productTypeId)
        {
            return await _dbApiContext.Surcharges.FirstOrDefaultAsync(s => s.ProductTypeId == productTypeId);
        }
        public async Task<IEnumerable<Surcharge>> GetAll()
        {
            return await _dbApiContext.Surcharges.ToListAsync();
        }
        public void Update(Surcharge surCharge)
        {
            
            _dbApiContext.Surcharges.Update(surCharge).State = EntityState.Modified;
        }
    }
}
