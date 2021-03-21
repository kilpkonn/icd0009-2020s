using System;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Domain.App.Car> Cars { get; set; } = null!;

        public DbSet<CarAccess> CarAccesses { get; set; } = null!;

        public DbSet<CarAccessType> CarAccessTypes { get; set; } = null!;

        public DbSet<CarErrorCode> CarErrorCodes { get; set; } = null!;

        public DbSet<CarMark> CarMarks { get; set; } = null!;

        public DbSet<CarModel> CarModels { get; set; } = null!;

        public DbSet<CarType> CarTypes { get; set; } = null!;

        public DbSet<GasRefill> GasRefills { get; set; } = null!;

        public DbSet<Track> Tracks { get; set; } = null!;

        public DbSet<TrackLocation> TrackLocations { get; set; } = null!;
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            //     .SelectMany(e => e.GetForeignKeys()))
            // {
            //     relationship.DeleteBehavior = DeleteBehavior.Restrict;
            // }

        }
    }
}