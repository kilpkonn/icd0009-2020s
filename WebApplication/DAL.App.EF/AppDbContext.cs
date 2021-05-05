using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
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

        public DbSet<LangString> LangStrings { get; set; } = null!;

        public DbSet<Translation> Translations { get; set; } = null!;
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Translation>()
                .HasKey(k => new { k.Culture, k.LangStringId});
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Domain.App.Car>()
                .HasMany(x => x.CarAccesses)
                .WithOne(x => x.Car!)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Domain.App.Car>()
                .HasMany(x => x.CarErrorCodes)
                .WithOne(x => x.Car!)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Domain.App.Car>()
                .HasMany(x => x.GasRefills)
                .WithOne(x => x.Car!)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Track>()
                .HasMany(x => x.TrackLocations)
                .WithOne(x => x.Track!)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarMark>()
                .HasMany(x => x.CarModels)
                .WithOne(x => x.CarMark!)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarModel>()
                .HasMany(x => x.CarTypes)
                .WithOne(x => x.CarModel!)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}