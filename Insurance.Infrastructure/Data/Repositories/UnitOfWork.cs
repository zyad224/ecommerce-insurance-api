using Insurance.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbApiContext _dbApiContext;
        public ISurchargeRepository SurchargeRepo { get; }

        public UnitOfWork(IDbApiContext dbApiContext, ISurchargeRepository surchargeRepo)
        {
            _dbApiContext = dbApiContext;
            SurchargeRepo = surchargeRepo;      
        }
        public async Task<int> Commit()
        {
            return await _dbApiContext.Commit();
        }
    }
}
