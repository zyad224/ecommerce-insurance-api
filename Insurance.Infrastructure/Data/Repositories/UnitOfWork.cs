using Insurance.Domain.Interfaces;
using System.Threading.Tasks;
namespace Insurance.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbApiContext _dbApiContext;
        public ISurchargeRepository SurchargeRepo { get; private set;}
        public UnitOfWork(IDbApiContext dbApiContext)
        {
            _dbApiContext = dbApiContext;
            SurchargeRepo = new SurchargeRepository(_dbApiContext);      
        }
        public async Task<int> Commit()
        {
            return await _dbApiContext.Commit();
        }
    }
}
