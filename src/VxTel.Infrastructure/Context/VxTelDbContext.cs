using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using VxTel.Domain.Common;
using VxTel.Domain.Entities;
using VxTel.Infrastructure.Mappings;

namespace VxTel.Infrastructure.Context
{
    public class VxTelDbContext : DbContext
    {
        public VxTelDbContext(DbContextOptions<VxTelDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Price> Prices { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<BaseTraceable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VxTelDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
