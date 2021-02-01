using ConferenceAPI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ConferenceAPI.Models
{
    public class VersionCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public VersionCheckMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {           
           var versionHeaderValue = "v1";
           var upsupportedHeaderValue = "1";
           if (context.Request.Headers.TryGetValue("Api-version", out var traceValue))
           {
                if (upsupportedHeaderValue.Equals(traceValue, StringComparison.OrdinalIgnoreCase))
                {
                    context.Request.Headers.Remove("Api-version");
                }

                if (versionHeaderValue.Equals(traceValue, StringComparison.OrdinalIgnoreCase))
                {
                    context.Request.Headers.Remove("Api-version");
                    context.Request.Headers.Add("Api-version", _appSettings.SupportedVersion);
                }
            }

            await _next(context);
        }
    }
}
