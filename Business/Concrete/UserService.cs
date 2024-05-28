using Business.Abstract;
using DTO.DTOs.UserDTOs;
using Entity;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Http;

namespace Business.Concrete
{
    public class UserService : GenericRepository<User>, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly EduContext _context;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, EduContext context) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        public UserService(EduContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> RegisterUser(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync(model.Role);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);

                    return ServiceResponse.Success("Kayıt işlemi başarılı");
                }
                else
                {
                    return ServiceResponse.Fail("Belirtilen rol bulunamadı");
                }
            }

            var exception = new Exception(string.Join(Environment.NewLine, result.Errors));

            return ServiceResponse.Fail("Kayıt işlemi başarısız", ex: exception);
        }

        public List<User> GetUserList()
        {
            var users = _userManager.Users.ToList();
            return users;
        }
    }
}
