using Business.Abstract;
using Business.Concrete;
using DTO.DTOs.UserDTOs;
using Entity;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using Utilities.Http;

namespace EducationManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [CustomAuthorize("Student", "Teacher")]
        [HttpPost("Register")]
        [Description("Tanımlı Roller Admin,Teacher,Student,Manager")]
        public async Task<ServiceResponse> Register([FromBody] RegisterModel model)
        {
            return await _userService.RegisterUser(model);
        }

        [CustomAuthorize("Manager")]
        [HttpGet("List")]
        public List<User> GetUsers()
        {
            return _userService.GetUserList();
        }
    }
}
