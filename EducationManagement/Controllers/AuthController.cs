using Business.Abstract;
using DTO.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Utilities.Http;

namespace EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILogonService _logonService;
        public AuthController(ILogonService logonService)
        {
            _logonService = logonService;
        }

        [HttpPost("Login")]
        [Description("<b>Tanımlı Kullanıcılar</b> <br> useradmin,userteacher,userstudent,usermanager <br> şifre <b>123</b>")]
        public async Task<ServiceResponse> Login([FromBody] LoginModel model)
        {
            return await _logonService.Login(model);
        }

        [HttpPost("Logout")] 
        public async Task<ServiceResponse> Logout()
        {
            return await _logonService.Logout();
        }

    }
}
