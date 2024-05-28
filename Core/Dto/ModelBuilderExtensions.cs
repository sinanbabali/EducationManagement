using Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Users tablosuna örnek veriler ekleme
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "1234567890",
                    PasswordHash = "hashed_password",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "0987654321",
                    PasswordHash = "hashed_password",
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@example.com",
                    PhoneNumber = "9876543210",
                    PasswordHash = "hashed_password",
                    RoleId = 3
                }
            );

            // Roles tablosuna örnek veriler ekleme

            modelBuilder.Entity<Role>().HasData(
          new Role { Id = 1, Name = "Admin" },
          new Role { Id = 2, Name = "Teacher" },
          new Role { Id = 3, Name = "Student" }
      );

            // UserRoles tablosuna örnek veriler ekleme
            modelBuilder.Entity<UserRole>().HasData(
new UserRole { Id = 1, UserId = 1, RoleId = 1 }, // John Doe
new UserRole { Id = 2, UserId = 2, RoleId = 2 }, // Jane Smith
new UserRole { Id = 3, UserId = 3, RoleId = 2 }
);
        }

    }
}
