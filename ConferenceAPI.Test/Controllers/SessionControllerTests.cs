using ConferenceAPI.Controllers;
using ConferenceAPI.Data;
using ConferenceAPI.Helper;
using ConferenceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.Threading;
using Moq;
using NUnit.Framework;
using ConferenceAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ConferenceAPI.Test.Controllers
{
    public class SessionControllerTests
    {
        private Mock<ISessionRepository> _mockSessionRepository;
        private IOptions<AppSettings> _mockappsetting;
        private SessionController _sessionController;
        private Mock<LinkGenerator> _linkGenerator;
        private DefaultHttpContext _defaultContext;
        private AppSettings appsettings;
        private List<Session> _sessions;

        [SetUp]
        public void SetUp()
       {
            _mockSessionRepository = new Mock<ISessionRepository>();
            _sessions = new List<Session> {
             new Session {
               SessionId =1,
               SessionName ="Async in C# - Session 1",
               SessionDetails = "Session deatils 1",
               SpeakerDetails = new Speaker {
               SpeakerId =1,
               Details ="This is speaker-1",
               Name = "Jon Skeet"
               },
               TimeSlotDetails = new TimeSlot {
               TimeSlotId =1,
               TimeSlotTime ="04 December 2013 11:40 - 12:40",
               Day =1
               },
               TopicDetails = new List<Topic> {
                new Topic {
                TopicId =1,
                Details ="Topic 1"
                },
                new Topic {
                TopicId =2,
                Details ="Topic 2"
                }
               }
             }
             };

            string speakername = "Test";
            int dayno = 1;
            string keyword = "test2";
            int id = 1;

            _mockSessionRepository.Setup(repo => repo.GetTopicsBySessionId(id))
                      .ReturnsAsync(_sessions.FirstOrDefault(re => re.SessionId==id));

            _mockSessionRepository.Setup(repo => repo.GetAllSessions(speakername,dayno,keyword))
                   .ReturnsAsync(_sessions);
            _linkGenerator = new Mock<LinkGenerator>();

            _mockappsetting = Options.Create(appsettings);
            _defaultContext = new DefaultHttpContext();
            _defaultContext.Request.Headers.Add("Api-version", "v1");
            _defaultContext.Request.Headers.Add("Ocp-Apim-Subscription-Key", "Test");

            _sessionController = new SessionController(_mockSessionRepository.Object,
                _linkGenerator.Object,
                _mockappsetting
                )
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = _defaultContext,
                }
            };

            appsettings = new AppSettings()
            {
               SpeakerInfoLink= "http://tavis.net/rels/speaker",
               TopicInfoLink= "http://tavis.net/rels/topics",
               SesssionLink= "http://tavis.net/rels/sessions",
               SupportedVersion= "1.0",
               Title= "Title",
               Timeslot= "Timeslot",
               Speaker= "Speaker"

            };

            
        }

        [Test]
        public async Task GetAllSessions_OK_ResponseType()
        {
            var result= await _sessionController.GetAllSessions("Test", 1,"test2");
            var okResult = result.Result as OkObjectResult;
            Assert.True(okResult.StatusCode == StatusCodes.Status200OK);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<SessionModel>(okResult.Value);            
        }

        [Test]
        public async Task GetAllSessions_NotFound_ResponseType()
        {
            var result = await _sessionController.GetAllSessions("", 1, "test2");
            var notFound = result.Result as NotFoundResult;
            Assert.True(notFound.StatusCode == StatusCodes.Status404NotFound );
        }

        [Test]
        public async Task GetTopicsForSession_OK_ResponseType()
        {
            
            var result = await _sessionController.GetTopicsForSession(1);
            var okResult = result.Result as OkObjectResult;
            Assert.True(okResult.StatusCode == StatusCodes.Status200OK);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOf<SessionModel>(okResult.Value);
        }

        [Test]
        public async Task GetTopicsForSession_NotFound_ResponseType()
        {
            var result = await _sessionController.GetTopicsForSession(2);
            var notFound = result.Result as NotFoundResult;
            Assert.True(notFound.StatusCode == StatusCodes.Status404NotFound);
        }
        

    }
}
;
