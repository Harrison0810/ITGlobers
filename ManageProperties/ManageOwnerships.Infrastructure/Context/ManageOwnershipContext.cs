using ManageOwnerships.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageOwnerships.Infrastructure.Context
{
    public class ManageOwnershipContext : DbContext
    {
        public ManageOwnershipContext(DbContextOptions<ManageOwnershipContext> options) : base(options)
        {
            Database.SetCommandTimeout(180);
        }

        public DbSet<OwnerEntity> Owner { get; set; }
        public DbSet<OwnershipEntity> Ownership { get; set; }
        public DbSet<OwnershipImageEntity> OwnershipImage { get; set; }
        public DbSet<OwnershipTraceEntity> OwnershipTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OwnerEntity>().ToTable("Owner");
            modelBuilder.Entity<OwnershipEntity>().ToTable("Ownership");
            modelBuilder.Entity<OwnershipImageEntity>().ToTable("OwnershipImage");
            modelBuilder.Entity<OwnershipTraceEntity>().ToTable("OwnershipTrace");
        }
    }
}
