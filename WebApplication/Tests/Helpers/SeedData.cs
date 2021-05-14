using System.Collections.Generic;
using DAL.App.EF;
using Domain.App;
using Domain.Base;

namespace Tests.Helpers
{
    public static class SeedData
    {
        public static void SeedAccessTypes(AppDbContext ctx)
        {
            ctx.CarAccessTypes.Add(new CarAccessType()
            {
                Name = "Owner",
                AccessLevel = 100,
            });
            ctx.SaveChanges();
        }
        public static void SeedTypes(AppDbContext ctx)
        {
            ctx.CarMarks.Add(new()
            {
                Name = new LangString("TestMark"),
                CarModels = new List<CarModel>
                {
                    new()
                    {
                        Name = new LangString("TestModel"),
                        CarTypes = new List<CarType>
                        {
                            new() { Name = new LangString("TestType") },
                            new() { Name = new LangString("TestType2")},
                        }
                    }
                }
            });
            ctx.SaveChanges();
        }
    }
}