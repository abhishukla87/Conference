using ConferenceAPI.Data;
using ConferenceAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
       

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings )
        {
            _next = next;
            _appSettings = appSettings.Value;
            
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            var token = context.Request.Headers["Ocp-Apim-Subscription-Key"].FirstOrDefault()?.Split(" ").Last();
            
            // remove header if hacker added authenticated header
            context.Request.Headers.Remove("Auth-User");
            
            if (token != null)
                attachUserToContext(context, userRepository, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context,
                                         IUserRepository userRepository,
                                         string token)
        {
            try
            {

                // Due to mockuser repository directly validating token and setting up mock user if "Ocp-Apim-Subscription-Key" header have value  

                var validateduser = userRepository.GetByUserName(token);

                context.Request.Headers.Add("Auth-User", validateduser.Username);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
