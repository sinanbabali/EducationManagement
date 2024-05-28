using Business.Concrete;
using Business.Interface;
using DTO.DTOs.UserDTOs;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utilities.Http;

namespace Business.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<ServiceResponse> RegisterUser(RegisterModel model);
        List<User> GetUserList();
    }
}
