using Business.Abstract;
using DTO.DTOs.UserDTOs;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Http;

namespace Business.Concrete
{
    public class LogonService : ILogonService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LogonService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public LogonService()
        {

        }

        public async Task<ServiceResponse> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ServiceResponse.Fail("Kullanıcı Bulunamadı");
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return ServiceResponse.Success("Giriş Başarılı");
            }
            else
            {
                return ServiceResponse.Fail("Geçersiz e-posta veya şifre");
            }
        }

        public async Task<ServiceResponse> Logout()
        {
            await _signInManager.SignOutAsync();

            return ServiceResponse.Success("Çıkış başarılı");
        }

    }
}
