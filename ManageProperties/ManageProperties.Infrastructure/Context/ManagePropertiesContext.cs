using ManageProperties.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageProperties.Infrastructure.Context
{
    public class ManagePropertiesContext : DbContext
    {
        public ManagePropertiesContext(DbContextOptions<ManagePropertiesContext> options) : base(options)
        {
            Database.SetCommandTimeout(180);
        }

        public DbSet<OwnerEntity> Owner { get; set; }
        public DbSet<PropertyEntity> Property { get; set; }
        public DbSet<PropertyImageEntity> PropertyImage { get; set; }
        public DbSet<PropertyTraceEntity> PropertyTrace { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OwnerEntity>().ToTable("Owner");
            modelBuilder.Entity<PropertyEntity>().ToTable("Property");
            modelBuilder.Entity<PropertyImageEntity>().ToTable("PropertyImage");
            modelBuilder.Entity<PropertyTraceEntity>().ToTable("PropertyTrace");
        }
    }
}
