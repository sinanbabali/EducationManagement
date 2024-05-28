using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace DTO.DTOs.UserDTOs
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = string.IsNullOrEmpty(value) ? "123" : value;
            }

        }
    }
}
