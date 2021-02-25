using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class AppDbContext: DbContext
    {
        public DbSet<Car> Cars { get; set; } = null!;

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
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


            // modelBuilder.Entity<Player>()
            //     .HasMany<GameSession>()
            //     .WithOne(x => x.PlayerWhite)
            //     .HasForeignKey(x => x.PlayerWhiteId)
            //     .OnDelete(DeleteBehavior.Restrict);
            //
            // modelBuilder.Entity<Player>()
            //     .HasMany<GameSession>()
            //     .WithOne(x => x.PlayerBlack)
            //     .HasForeignKey(x => x.PlayerBlackId)
            //     .OnDelete(DeleteBehavior.Restrict);
            //
            // modelBuilder.Entity<GameSession>()
            //     .HasMany<BoardState>()
            //     .WithOne(x => x.GameSession)
            //     .HasForeignKey(x => x.GameSessionId)
            //     .OnDelete(DeleteBehavior.Cascade);
            //
            // modelBuilder.Entity<BoardState>()
            //     .HasMany<BoardTile>()
            //     .WithOne(x => x.BoardState)
            //     .HasForeignKey(x => x.BoardStateId)
            //     .OnDelete(DeleteBehavior.Cascade);
            //
            // modelBuilder.Entity<BoardTile>()
            //     .HasIndex(x => x.CoordX);
            //
            // modelBuilder.Entity<BoardTile>()
            //     .HasIndex(x => x.CoordY);

        }
    }
}