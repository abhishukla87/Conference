using ConferenceAPI.Data;
using ConferenceAPI.Data.Entities;
using ConferenceAPI.Helper;
using ConferenceAPI.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceAPI.Test
{
    class JwtMiddlewareTests
    {
        private IOptions<AppSettings> _mockappsetting;
        private Mock<IUserRepository> _mockUserRepository;
        private User _testuser;
        private AppSettings _appsettings;
        private DefaultHttpContext defaultContext;
        private string username;

        [SetUp]
        public void Setup()
        {
            _appsettings = new AppSettings()
            {
                SupportedVersion = "1.0",
            };
            _mockappsetting = Options.Create(_appsettings);
            _mockUserRepository = new Mock<IUserRepository>();
            _testuser = new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                Username = "test"
            };
           
        }

        [Test]
        public async Task JwtMiddleware_ValidUser()
        {
             username = "test";

            _mockUserRepository.Setup(repo => repo.GetByUserName(username)).
                                     Returns(_testuser.Username == username ? _testuser : null);

            defaultContext = new DefaultHttpContext();           
            defaultContext.Request.Path = "/";
            defaultContext.Request.Headers.Add("Ocp-Apim-Subscription-Key", "test");
            
            var middlewareInstance = new JwtMiddleware(next:
                (innerHttpContext) => {
                    return Task.CompletedTask;
                },
                    _mockappsetting
                    );

           await middlewareInstance.Invoke(defaultContext, _mockUserRepository.Object);                      
           Assert.True(defaultContext.Request.Headers["Auth-User"].FirstOrDefault()?.Split(" ").Last() != null);


        }

        [Test]
        public async Task JwtMiddleware_InValidUser()
        {
            username = "test2";

            _mockUserRepository.Setup(repo => repo.GetByUserName(username)).Returns(_testuser.Username == username ? _testuser : null);

            defaultContext = new DefaultHttpContext();
           
            defaultContext.Request.Path = "/";

            var middlewareInstance = new JwtMiddleware(next:
                (innerHttpContext) => {
                    return Task.CompletedTask;
                },
                    _mockappsetting
                    );

          await  middlewareInstance.Invoke(defaultContext, _mockUserRepository.Object);

            var test = defaultContext.Request.Headers["Auth-User"].Count;

          Assert.True(defaultContext.Request.Headers["Auth-User"].Count == 0);


        }
    }
}
