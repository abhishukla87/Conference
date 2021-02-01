using ConferenceAPI.Helper;
using ConferenceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceAPI.Test
{
    class VersionCheckMiddlewareTests
    {
        private IOptions<AppSettings> _mockappsetting;
        private AppSettings _appsettings;
        private DefaultHttpContext _defaultContext;
        
        [SetUp]
        public void Setup()
        {
            _appsettings = new AppSettings()
            {
                SupportedVersion = "1.0",

            };
            _mockappsetting = Options.Create(_appsettings);
        }

        [Test]
        public async Task VersionCheckMiddleware_ValidVersion()
        {   
            _defaultContext = new DefaultHttpContext();
            _defaultContext.Request.Headers.Add("Api-version", "v1");
            _defaultContext.Request.Path = "/";

            var middlewareInstance = new VersionCheckMiddleware(next:
                (innerHttpContext) => {
                    return Task.CompletedTask; },
                    _mockappsetting
                    );

            await  middlewareInstance.Invoke(_defaultContext);

            Assert.True(_appsettings.SupportedVersion.Equals(_defaultContext.Request.Headers["Api-version"].FirstOrDefault()?.Split(" ").Last()));
        }

        [Test]
        public async Task VersionCheckMiddlewareTest_InValidVersion()
        {
            _defaultContext = new DefaultHttpContext();
            _defaultContext.Request.Headers.Add("Api-version", "v2");
            _defaultContext.Request.Path = "/";

            var middlewareInstance = new VersionCheckMiddleware(next:
                (innerHttpContext) => {
                    return Task.CompletedTask;
                },
                    _mockappsetting
               );

          await  middlewareInstance.Invoke(_defaultContext);

          Assert.False(_appsettings.SupportedVersion.Equals(_defaultContext.Request.Headers["Api-version"].FirstOrDefault()?.Split(" ").Last()));
        }
    }
}
