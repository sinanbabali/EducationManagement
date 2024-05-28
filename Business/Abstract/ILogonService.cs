using DTO.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Http;

namespace Business.Abstract
{
    public interface ILogonService
    {
        Task<ServiceResponse> Login(LoginModel model);
        Task<ServiceResponse> Logout();
    }
}
