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
    }
}