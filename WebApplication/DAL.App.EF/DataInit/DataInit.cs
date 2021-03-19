using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.DataInit
{
    public static class DataInit
    {
        public static void DropDatabase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }

        public static void SeedAppData(AppDbContext ctx)
        {
            var user = ctx.Users.First();

            var markHyundai = new CarMark() {Name = "Hyundai", CreatedBy = user.Id};
            var markHonda = new CarMark() {Name = "Honda", CreatedBy = user.Id};
            var markToyota = new CarMark() {Name = "Toyota", CreatedBy = user.Id};
            ctx.CarMarks.Add(markHyundai);
            ctx.CarMarks.Add(markHonda);
            ctx.CarMarks.Add(markToyota);

            var modelI20 = new CarModel() {Name = "i20", CarMark = markHyundai};
            var modelI30 = new CarModel() {Name = "i30", CarMark = markHyundai};
            var modelI40 = new CarModel() {Name = "i40", CarMark = markHyundai};
            ctx.CarModels.Add(modelI20);
            ctx.CarModels.Add(modelI30);
            ctx.CarModels.Add(modelI40);

            var civic = new CarModel() {Name = "Civic", CarMark = markHonda};
            var jazz = new CarModel() {Name = "Jazz", CarMark = markHonda};
            var accord = new CarModel() {Name = "Accord", CarMark = markHonda};
            ctx.CarModels.Add(civic);
            ctx.CarModels.Add(jazz);
            ctx.CarModels.Add(accord);

            var yaris = new CarModel() {Name = "Yaris", CarMark = markToyota};
            var auris = new CarModel() {Name = "Auris", CarMark = markToyota};
            ctx.CarModels.Add(yaris);
            ctx.CarModels.Add(auris);

            CreateTypesForModel(modelI20, ctx);
            CreateTypesForModel(modelI30, ctx);
            CreateTypesForModel(modelI40, ctx);
            CreateTypesForModel(civic, ctx);
            CreateTypesForModel(jazz, ctx);
            CreateTypesForModel(accord, ctx);
            CreateTypesForModel(yaris, ctx);
            CreateTypesForModel(auris, ctx);
            
            ctx.SaveChanges();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleAdmin = new AppRole {Name = "Admin"};
            var roleUser = new AppRole() {Name = "User"};
            var resultAdmin = roleManager.CreateAsync(roleAdmin).Result;
            var resultUser = roleManager.CreateAsync(roleUser).Result;
            if (!resultAdmin.Succeeded || !resultUser.Succeeded)
            {
                foreach (var identityError in resultAdmin.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
                foreach (var identityError in resultUser.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }

            var user = new AppUser();
            user.Email = "tavo.annus@gmail.com";
            user.UserName = user.Email;
            user.DisplayName = "Tavo Annus";

            var result = userManager.CreateAsync(user, "Password1_").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }

            result = userManager.AddToRoleAsync(user, "Admin").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }
        }

        private static void CreateTypesForModel(CarModel model, AppDbContext ctx)
        {
            for (int i = 0; i < 3; i++)
            {
                var type = new CarType() { Name = $"20{10 + i * 3}", CarModel = model};
                model.CarTypes.Add(type);
                ctx.CarTypes.Add(type);
            }
        }
    }

}