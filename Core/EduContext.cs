using Core.Dto;
using Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class EduContext : DbContext
    {
        public EduContext(DbContextOptions<EduContext> options)
           : base(options)
        {
        }
        public EduContext() { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Selection> Selections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "EduDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
       .HasOne(u => u.Role)
       .WithMany(r => r.Users)
       .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.TeacherId);

            modelBuilder.Entity<Selection>()
                .HasOne(e => e.Student)
                .WithMany(u => u.Selections)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Selection>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Selections)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Seed();




            base.OnModelCreating(modelBuilder);

        }
    }
}

