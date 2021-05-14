using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using CarApp.BLL.App.Services;
using CarApp.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.DTO.MappingProfiles;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Car = Domain.App.Car;

namespace Test.UnitTests
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
            // provide new random database name here
            // or parallel test methods will conflict each other
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _ctx = new AppDbContext(optionBuilder.Options);
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            var dalMapperCfg = GetDalMapperConfiguration();
            var bllMapperCfg = GetBllMapperConfiguration();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            _service = new BLL.App.Services.CarService(
                new AppUnitOfWork(_ctx, new Mapper(dalMapperCfg)),
                new CarRepository(_ctx, new Mapper(dalMapperCfg)),
                new Mapper(bllMapperCfg));
        }
        
        [Fact]
        public async Task Action_Test__GetAllForUser()
        {
            // ARRANGE
            var user = _ctx.Users.Add(new AppUser()
            {
                DisplayName = "TestUser",
                PasswordHash = "PassHash123",
                Email = "test@test.test"
            }).Entity;
            var car = _ctx.Cars.Add(new Domain.App.Car
            {
                CarTypeId = _ctx.CarTypes.FirstAsync().Result.Id,
                CarAccesses = new List<Domain.App.CarAccess>
                {
                    new() {AppUser = user}
                }
            }).Entity;
            await _ctx.SaveChangesAsync();
            
            // ACT
            var result = await _service.GetAccessibleCarsForUser(user.Id);
            
            // ASSERT
            Assert.NotNull(result);
            // _testOutputHelper.WriteLine($"Count of elements: {testVm.ContactTypes.Count}");
            var enumerable = result.ToList();
            Assert.Single(enumerable!);
            Assert.Equal("TestType", enumerable.First().CarType!.Name);
        }


        private static MapperConfiguration GetBllMapperConfiguration()
        {
            return new(cfg =>
            {
                cfg.CreateMap<BLL.App.DTO.Car, DAL.App.DTO.Car>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.CarAccess, DAL.App.DTO.CarAccess>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.CarAccessType, DAL.App.DTO.CarAccessType>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.CarErrorCode, DAL.App.DTO.CarErrorCode>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.CarMark, DAL.App.DTO.CarMark>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.CarModel, DAL.App.DTO.CarModel>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.CarType, DAL.App.DTO.CarType>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.GasRefill, DAL.App.DTO.GasRefill>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.Track, DAL.App.DTO.Track>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.TrackLocation, DAL.App.DTO.TrackLocation>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.Identity.AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
                cfg.CreateMap<BLL.App.DTO.Identity.AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
            });
        }

        private static MapperConfiguration GetDalMapperConfiguration()
        {
            return new(cfg =>
            {
                cfg.CreateMap<DAL.App.DTO.Car, Domain.App.Car>().ReverseMap();
                cfg.CreateMap<CarAccess, Domain.App.CarAccess>().ReverseMap();
                cfg.CreateMap<CarAccessType, Domain.App.CarAccessType>().ReverseMap();
                cfg.CreateMap<CarErrorCode, Domain.App.CarErrorCode>().ReverseMap();
                cfg.CreateMap<CarMark, Domain.App.CarMark>().ReverseMap();
                cfg.CreateMap<CarModel, Domain.App.CarModel>().ReverseMap();
                cfg.CreateMap<CarType, Domain.App.CarType>().ReverseMap();
                cfg.CreateMap<GasRefill, Domain.App.GasRefill>().ReverseMap();
                cfg.CreateMap<Track, Domain.App.Track>().ReverseMap();
                cfg.CreateMap<TrackLocation, Domain.App.TrackLocation>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
                cfg.CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
            });
        }
    }
}