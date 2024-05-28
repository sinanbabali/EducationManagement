using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace Utilities.Helpers
{
    public class SwaggerOperationDescriptionFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                var methodDescription = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
                    .OfType<DescriptionAttribute>()
                    .FirstOrDefault();

                if (methodDescription != null)
                {
                    operation.Description = methodDescription.Description;
                }
            }
        }
    }
}
