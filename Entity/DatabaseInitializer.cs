using Entity.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public static class DatabaseInitializer
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Role>>();

            var Roles = new List<Role>
            {
                 new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                 new Role { Id = 2, Name = "Teacher", NormalizedName = "TEACHER" },
                 new Role { Id = 3, Name = "Student", NormalizedName = "STUDENT" },
                 new Role { Id = 4, Name = "Manager", NormalizedName = "MANAGER"}
            };

            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var hasher = new PasswordHasher<User>();

            var Users = new List<User>
            {
            new User
            {
                UserName = "admin",
                Email = "useradmin",
                FirstName = "Admin",
                LastName = "User",
                PhoneNumber = "1234567890",
                PasswordHash = hasher.HashPassword(null, "123")
            },
            new User
            {
                UserName = "teacher",
                Email = "userteacher",
                FirstName = "Teacher",
                LastName = "User",
                PhoneNumber = "1234567890",
                PasswordHash = hasher.HashPassword(null, "123")
            },
            new User
            {
                UserName = "student",
                Email = "userstudent",
                FirstName = "Student",
                LastName = "User",
                PhoneNumber = "1234567890",
                PasswordHash = hasher.HashPassword(null, "123")
            },
              new User
            {
                UserName = "manager",
                Email = "usermanager",
                FirstName = "Manager",
                LastName = "User",
                PhoneNumber = "1234567890",
                PasswordHash = hasher.HashPassword(null, "123")
            }
            };

            foreach (var user in Users)
            {
                await userManager.CreateAsync(user);
            }


            foreach (var user in Users)
            {
                if (user.UserName == "admin")
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else if (user.UserName == "teacher")
                {
                    await userManager.AddToRoleAsync(user, "Teacher");
                }
                else if (user.UserName == "student")
                {
                    await userManager.AddToRoleAsync(user, "Student");
                }
                else if (user.UserName == "manager")
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                }
            }
        }
    }
}
