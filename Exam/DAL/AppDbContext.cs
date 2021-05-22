using System;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
        // IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, 
        //     IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public DbSet<Declaration> Declarations { get; set; } = default!;
        public DbSet<Grade> Grades { get; set; } = default!;
        public DbSet<Homework> Homeworks { get; set; } = default!;
        public DbSet<LecturerSubject> LecturerSubjects { get; set; } = default!;
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<QuizAnswer> QuizAnswers { get; set; } = default!;
        public DbSet<QuizOption> QuizOptions { get; set; } = default!;
        public DbSet<QuizQuestion> QuizQuestions { get; set; } = default!;
        public DbSet<Semester> Semesters { get; set; } = default!;
        public DbSet<Subject> Subjects { get; set; } = default!;
        public DbSet<Submission> Submissions { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }


        }
    }
}