using System.Collections.Generic;
using System.Linq;
using DAL.App.EF;
using Domain.App;
using Domain.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the db context
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase("testdb");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();
                db.CarMarks.Add(new()
                {
                    Name = new LangString("TestMark"),
                    CarModels = new List<CarModel>
                    {
                        new()
                        {
                            Name = new LangString("TestModel"),
                            CarTypes = new List<CarType>
                            {
                                new() { Name = new LangString("TestType") }
                            }
                        }
                    }
                });
            });
        }
    }
}