using Microsoft.EntityFrameworkCore;
using DapperPatterns.AircraftTypes;
using DapperPatterns.Airplanes;

namespace DapperPatterns.Dapper
{
    public sealed class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AircraftType> AircraftTypes { get; set; }

        public DbSet<Airplane> Airplanes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airplane>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Registration);
                x.HasOne(y => y.AircraftType).WithMany().IsRequired();
                //x.HasData(new Airplane(Guid.NewGuid(), "N231NA", new AircraftType(Guid.NewGuid(), "Boeing", "737-8", 174)));
            });

            modelBuilder.Entity<AircraftType>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Manufacturer);
                x.Property(y => y.Model);
                x.Property(y => y.Seats);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
