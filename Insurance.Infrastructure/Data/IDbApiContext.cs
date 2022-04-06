using Insurance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Insurance.Infrastructure.Data
{
    public interface IDbApiContext
    {
        public DbSet<Surcharge> Surcharges { get; set; }
        public Task<int> Commit();

    }
}
