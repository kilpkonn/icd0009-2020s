using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.DTO;
using BLL.App.Services;
using CarApp.BLL.App.Services;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tests.Helpers;
using Xunit;
using Xunit.Abstractions;
using AppRole = BLL.App.DTO.Identity.AppRole;

namespace Tests.UnitTests
{
    public class CarServiceTests
    {
        private readonly ICarService _service;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly AppDbContext _ctx;

        // ARRANGE - common
        public CarServiceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            // set up db context for testing - using InMemory db engine
            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.EnableSensitiveDataLogging(true);
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            _ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _ctx.ChangeTracker.AutoDetectChangesEnabled = false;
            var dalMapperCfg = GetDalMapperConfiguration();
            var bllMapperCfg = GetBllMapperConfiguration();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            _service = new CarService(
                new AppUnitOfWork(_ctx, new Mapper(dalMapperCfg)),
                new CarRepository(_ctx, new Mapper(dalMapperCfg)),
                new Mapper(bllMapperCfg));
        }
        
        [Fact]
        public async Task Action_Test__GetAllForUser()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.First().Id,
            }, _ctx.Users.First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var result = await _service.GetAccessibleCarsForUser(_ctx.Users.First().Id);
            
            // ASSERT
            Assert.NotNull(result);
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.ContactTypes.Count}");
            var enumerable = result.ToList();
            Assert.Single(enumerable!);
            Assert.Equal("TestType", enumerable.First().CarType!.Name);
        }
        
        [Fact]
        public async Task Action_Test__UpdateCar()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.AsNoTracking().First().Id,
            }, _ctx.Users.AsNoTracking().First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = _service.GetAccessibleCarsForUser(_ctx.Users.First().Id).Result.First();
            DetachAllEntities();
            var result = await _service.UpdateAsync(new BLL.App.DTO.Car()
            {
                Id = res.Id,
                CarTypeId = _ctx.CarTypes.AsNoTracking().Last().Id
            }, _ctx.Users.First().Id);
            
            
            // ASSERT
            Assert.NotNull(result);
            res = _service.GetAccessibleCarsForUser(_ctx.Users.First().Id).Result.FirstOrDefault();
            Assert.NotNull(res);
            Assert.Equal("TestType", res!.CarType!.Name);
        }
        
        
        [Fact]
        public async Task Action_Test__RemoveCarAsync()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.AsNoTracking().First().Id,
            }, _ctx.Users.AsNoTracking().First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = _service.GetAccessibleCarsForUser(_ctx.Users.First().Id).Result.First();
            DetachAllEntities();
            var result = await _service.RemoveAsync(res!.Id, _ctx.Users.First().Id);
            await _ctx.SaveChangesAsync();
            
            // ASSERT
            Assert.NotNull(result);
            res = _service.GetAccessibleCarsForUser(_ctx.Users.First().Id).Result.FirstOrDefault();
            Assert.Null(res);
        }
        
        [Fact]
        public async Task Action_Test__RemoveCar()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.AsNoTracking().First().Id,
            }, _ctx.Users.AsNoTracking().First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = _service.GetAccessibleCarsForUser(_ctx.Users.First().Id).Result.First();
            DetachAllEntities();
            var result = _service.Remove(res!, _ctx.Users.First().Id);
            await _ctx.SaveChangesAsync();
            
            // ASSERT
            Assert.NotNull(result);
            res = _service.GetAccessibleCarsForUser(_ctx.Users.First().Id).Result.FirstOrDefault();
            Assert.Null(res);
        }
        
        [Fact]
        public async Task Action_Test__GetAllCars()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.AsNoTracking().First().Id,
            }, _ctx.Users.AsNoTracking().First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = _service.GetAllAsync(_ctx.Users.First().Id).Result.First();
            
            
            // ASSERT
            Assert.NotNull(res);
            Assert.NotNull(res.CarTypeId);
            Assert.Null(res.CarType);
        }
        
        [Fact]
        public async Task Action_Test__FirstOrDefaultCar()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.AsNoTracking().First().Id,
            }, _ctx.Users.AsNoTracking().First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = _service.GetAllAsync(_ctx.Users.First().Id).Result.First();
            var res2 = await _service.FirstOrDefaultAsync(res.Id, _ctx.Users.First().Id);
            
            
            // ASSERT
            Assert.NotNull(res);
            Assert.NotNull(res2);
            Assert.Equal(res.Id.ToString(), res2!.Id.ToString());
        }
        
        [Fact]
        public async Task Action_Test__ExsistsCar()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.AsNoTracking().First().Id,
            }, _ctx.Users.AsNoTracking().First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res = _service.GetAllAsync(_ctx.Users.First().Id).Result.First();
            var res2 = await _service.ExistsAsync(res.Id, _ctx.Users.First().Id);
            
            
            // ASSERT
            Assert.NotNull(res);
            Assert.True(res2);
        }
        
        [Fact]
        public async Task Action_Test__GetUpdatedEntity()
        {
            // ARRANGE
            _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            });
            SeedData.SeedTypes(_ctx);
            SeedData.SeedAccessTypes(_ctx);
            var res = await _service.AddAsync(new BLL.App.DTO.Car()
            {
                CarTypeId = _ctx.CarTypes.AsNoTracking().First().Id,
            }, _ctx.Users.AsNoTracking().First().Id);
            await _ctx.SaveChangesAsync();
            
            // ACT
            var res2 = _service.GetUpdatedEntityAfterSaveChanges(res);
            
            
            // ASSERT
            Assert.NotNull(res);
            Assert.NotNull(res2);
            Assert.Equal(res.Id, res2.Id);
        }


        private static MapperConfiguration GetBllMapperConfiguration()
        {
            return new(cfg =>
            {
                cfg.CreateMap<BLL.App.DTO.Car, DAL.App.DTO.Car>().ReverseMap();
                cfg.CreateMap<CarAccess, DAL.App.DTO.CarAccess>().ReverseMap();
                cfg.CreateMap<CarAccessType, DAL.App.DTO.CarAccessType>().ReverseMap();
                cfg.CreateMap<CarErrorCode, DAL.App.DTO.CarErrorCode>().ReverseMap();
                cfg.CreateMap<CarMark, DAL.App.DTO.CarMark>().ReverseMap();
                cfg.CreateMap<CarModel, DAL.App.DTO.CarModel>().ReverseMap();
                cfg.CreateMap<CarType, DAL.App.DTO.CarType>().ReverseMap();
                cfg.CreateMap<GasRefill, DAL.App.DTO.GasRefill>().ReverseMap();
                cfg.CreateMap<Track, DAL.App.DTO.Track>().ReverseMap();
                cfg.CreateMap<TrackLocation, DAL.App.DTO.TrackLocation>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
                cfg.CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
            });
        }

        private static MapperConfiguration GetDalMapperConfiguration()
        {
            return new(cfg =>
            {
                cfg.CreateMap<DAL.App.DTO.Car, Domain.App.Car>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.CarAccess, Domain.App.CarAccess>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.CarAccessType, Domain.App.CarAccessType>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.CarErrorCode, Domain.App.CarErrorCode>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.CarMark, Domain.App.CarMark>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.CarModel, Domain.App.CarModel>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.CarType, Domain.App.CarType>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.GasRefill, Domain.App.GasRefill>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.Track, Domain.App.Track>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.TrackLocation, Domain.App.TrackLocation>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.Identity.AppUser, AppUser>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
            });
        }
        
        private void DetachAllEntities()
        {
            var changedEntriesCopy = this._ctx.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
            
            _ctx.ChangeTracker.Clear();
        }
    }
}