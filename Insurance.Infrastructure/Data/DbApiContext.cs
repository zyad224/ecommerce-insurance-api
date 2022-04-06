using Insurance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
           .Property(p => p.RowVersion)
           .IsRowVersion();
            modelBuilder.Entity<Surcharge>()
           .HasIndex(p => p.ProductTypeId)
           .IsUnique(); 
        }
        public async Task<int> Commit()
        {
            return await SaveChangesAsync();
        }
    }
}
