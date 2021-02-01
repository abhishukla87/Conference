using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ConferenceAPI.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Auth-User"))
            {              
                context.Result = new JsonResult(new 
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    message = "Access denied due to invalid subscription key. Make sure to provide a valid key for an active subscription." })
                { 
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}
