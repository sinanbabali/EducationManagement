using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Http
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles)
            : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { roles };
        }
    }

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly string[] _roles;

        public CustomAuthorizeFilter(string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasRole = false;

            if (context.HttpContext.User.IsInRole("Admin"))
            {
                return;
            }

            foreach (var role in _roles)
            {
                if (context.HttpContext.User.IsInRole(role))
                {
                    hasRole = true;
                    break;
                }
            }

            if (!hasRole)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}
