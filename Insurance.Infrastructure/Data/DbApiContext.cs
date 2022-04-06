using Insurance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace Insurance.Infrastructure.Data
{
    public class DbApiContext: DbContext, IDbApiContext
    {
        public DbApiContext(DbContextOptions<DbApiContext> options)
        : base(options)
        {
        }
        public DbSet<Surcharge> Surcharges { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Surcharge>()
            .HasKey(p=> p.Id );
            modelBuilder.Entity<Surcharge>()
           .Property(p => p.RowVersion)
           .IsRowVersion();
            modelBuilder.Entity<Surcharge>()
           .HasIndex(p => p.ProductTypeId)
           .IsUnique();
            modelBuilder.Entity<Insurance.Domain.Entities.Insurance>()
           .HasKey(p => p.Id);
            modelBuilder.Entity<Insurance.Domain.Entities.Insurance>().Ignore(p => p.IsSurCharge);
            modelBuilder.Entity<Insurance.Domain.Entities.Insurance>().Ignore(p => p.SurChargeFees);
        }
        public async Task<int> Commit()
        {
            return await SaveChangesAsync();
        }
    }
}
