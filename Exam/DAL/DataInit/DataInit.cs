using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataInit
{
    public class DataInit
    {
                
        public static void DropDatabase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }

        public static void SeedAppData(AppDbContext ctx, UserManager<AppUser> userManager)
        {
            var subject = new Subject()
            {
                Title = "Distributed",
                Description = "Description"
            };
            var semester1 = new Semester()
            {
                Name = "2020f",
                StartDate = new DateTime(2020, 09, 01),
                EndDate = new DateTime(2021, 01, 26),
            };
            
            var semester2 = new Semester()
            {
                Name = "2020s",
                StartDate = new DateTime(2021, 01, 27),
                EndDate = new DateTime(2021, 06, 30),
                Subjects = new List<Subject>()
                {
                    subject
                }
            };
            
            var semester3 = new Semester()
            {
                Name = "2021f",
                StartDate = new DateTime(2021, 09, 01),
                EndDate = new DateTime(2022, 01, 26),
            };
            
            var semester4 = new Semester()
            {
                Name = "2021s",
                StartDate = new DateTime(2022, 01, 27),
                EndDate = new DateTime(2022, 06, 30),
            };

            ctx.Semesters.Add(semester1);
            ctx.Semesters.Add(semester2);
            ctx.Semesters.Add(semester3);
            ctx.Semesters.Add(semester4);

            ctx.SaveChanges();
            
            var declarationSubmitted = new Declaration()
            {
                AppUser = userManager.FindByEmailAsync("student@test.com").Result,
                Subject = subject,
                DeclarationStatus = EDeclarationStatus.Accepted
            };
            
            var declarationAccepted = new Declaration()
            {
                AppUser = userManager.FindByEmailAsync("student2@test.com").Result,
                Subject = subject,
                DeclarationStatus = EDeclarationStatus.Accepted
            };
            ctx.Declarations.Add(declarationSubmitted);
            ctx.Declarations.Add(declarationAccepted);
           
            
            
            var homework1 = new Homework()
            {
                Subject = subject,
                Title = "HW1",
                Description = "distributed",
                Submissions = new List<Submission>()
                {
                    new Submission()
                    {
                        Value = "Submission1",
                        Grade = new Grade()
                        {
                            AppUser = userManager.FindByEmailAsync("student2@test.com").Result,
                            GradeType = EGradeType.Submission,
                            Subject = subject,
                            Value = 4,
                        }
                    }
                }
            };
            var homework2 = new Homework()
            {
                Title = "HW2",
                Description = "js",
                Subject = subject,
            };
            
            ctx.Homeworks.Add(homework1);
            ctx.Homeworks.Add(homework2);
            
            var subjectGrade = new Grade()
            {
                AppUser = userManager.FindByEmailAsync("student2@test.com").Result,
                GradeType = EGradeType.Subject,
                Subject = subject,
                Value = 4,
            };
            ctx.Grades.Add(subjectGrade);
            
            ctx.LecturerSubjects.Add(new LecturerSubject()
            {
                Subject = subject,
                AppUser = userManager.FindByEmailAsync("lecturer@test.com").Result
            });


            ctx.SaveChanges();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var role = new AppRole();
            role.Name = "Admin";
            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }
            
            var role2 = new AppRole();
            role2.Name = "Student";
            result = roleManager.CreateAsync(role2).Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }
            
            var role3 = new AppRole();
            role3.Name = "Lecturer";
            result = roleManager.CreateAsync(role3).Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }

            var user = new AppUser();
            user.Email = "admin@test.com";
            user.UserName = user.Email;
            user.EmailConfirmed = true;
            user.Id = new Guid();
            // user.Id = new Guid("adeb45e6-acdb-441d-f750-08d906538f99");
            
            result = userManager.CreateAsync(user, "Foo.bar1").Result;
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
            
            var userLecturer = new AppUser();
            userLecturer.Email = "lecturer@test.com";
            userLecturer.UserName = userLecturer.Email;
            userLecturer.EmailConfirmed = true;
            userLecturer.Id = new Guid();
            
            result = userManager.CreateAsync(userLecturer, "Foo.bar1").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }
            
            result = userManager.AddToRoleAsync(userLecturer, "Lecturer").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }
            
            var userStudent = new AppUser();
            userStudent.Email = "student@test.com";
            userStudent.UserName = userStudent.Email;
            userStudent.EmailConfirmed = true;
            userStudent.Id = new Guid();
            // user.Id = new Guid("adeb45e6-acdb-441d-f750-08d906538f99");
            
            result = userManager.CreateAsync(userStudent, "Foo.bar1").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }
            
            result = userManager.AddToRoleAsync(userStudent, "Student").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }
            
            var userStudent2 = new AppUser();
            userStudent2.Email = "student2@test.com";
            userStudent2.UserName = userStudent2.Email;
            userStudent2.EmailConfirmed = true;
            userStudent2.Id = new Guid();
            // user.Id = new Guid("adeb45e6-acdb-441d-f750-08d906538f99");
            
            result = userManager.CreateAsync(userStudent2, "Foo.bar1").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }
            
            result = userManager.AddToRoleAsync(userStudent2, "Student").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }
        }
    }
}
