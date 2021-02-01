using ConferenceAPI.Data;
using ConferenceAPI.Data.Entities;
using ConferenceAPI.Helper;
using ConferenceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConferenceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("conference")]
    [ApiVersion("1.0")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;   
        private readonly LinkGenerator _linkGenerator;
        private readonly AppSettings _appSettings;

        public SessionController(ISessionRepository repository,                                 
                                 LinkGenerator linkGenerator,
                                 IOptions<AppSettings> appSettings)
        {
            _sessionRepository = repository;          
            _linkGenerator = linkGenerator;
            _appSettings = appSettings.Value;
        }

      
        [Route("session/{id:int}" , Name ="GetSessionById")]
        [HttpGet]
        public IActionResult GetSession(int id)
        {
            try
            {
                //not implemented 
                return Ok();
              
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Issue in processing request");
            }
        }

        
        [Route("sessions")]
        [HttpGet("{speakername}, {dayno:int},{keywork}")]
        public async Task<ActionResult<SessionModel>> GetAllSessions(string speakername = "", int dayno = 0, string keyword= "" )
        {
            try
            {
                var result = await _sessionRepository.GetAllSessions(speakername, dayno, keyword);

                if (result != null)
                {
                    return Ok(DecorateSessionsResponse(result)); ;
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Issue in processing request");
            }
           
        }
             
                
        [Route("session/{id:int}/topics" , Name = "GetTopicsForSession")]
        [HttpGet]
        public async Task<ActionResult<SessionModel>> GetTopicsForSession(int id)
        {
            try
            {
                var result = await _sessionRepository.GetTopicsBySessionId(id);

                if (result != null)
                {
                    return Ok(DecorateTopicsResponse(result)); ;
                }
                else
                {
                    return NotFound(); 
                }               
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Issue in processing request.");
            }
        }      

        
        [Route("session/{id:int}/feedback")]
        [HttpPost]
        public ActionResult Post(int id)
        {
            //not implemented 
            return Ok();
        }

        private SessionModel DecorateSessionsResponse(List<Session> lstSession)
        {
            var sessionModel = new SessionModel();
            var collection = new CollectionModel();
            var lstItemsModel = new List<ItemsModel>();
          
            foreach (var session in lstSession)
            {
               
                var itemsModel = new ItemsModel
                {
                    Href = _linkGenerator.GetUriByRouteValues(HttpContext, "GetSessionById",
                                                              new { id = session.SessionId }
                    )                
                };
               
                var lstdataModel = new List<DataModel>();
                var lstLinkModel = new List<LinkModel>();

                var speaker = new DataModel
                {
                    Name = _appSettings.Speaker,
                    Value = session.SpeakerDetails.Name
                };
                lstdataModel.Add(speaker);
                var sessionDetail = new DataModel
                {
                    Name = _appSettings.Title,
                    Value = session.SessionName
                };
                lstdataModel.Add(sessionDetail);
                var timeSlot = new DataModel
                {
                    Name = _appSettings.Timeslot,
                    Value = session.TimeSlotDetails.TimeSlotTime
                };
                lstdataModel.Add(timeSlot);
                itemsModel.Data = lstdataModel;
                var speakerlink = new LinkModel
                {
                    Rel = _appSettings.SpeakerInfoLink,
                    Href = _linkGenerator.GetUriByRouteValues(HttpContext, "GetSpeakerById", new { id = session.SpeakerDetails.SpeakerId })
                };
                ;
                lstLinkModel.Add(speakerlink);

                var topicLink = new LinkModel
                {
                    Rel = _appSettings.TopicInfoLink,
                    Href = _linkGenerator.GetUriByRouteValues(HttpContext, "GetTopicsForSession", new { id = session.SessionId })
                };

                lstLinkModel.Add(topicLink);
                itemsModel.Links = lstLinkModel;                            
                lstItemsModel.Add(itemsModel);
            }

            collection.Items = lstItemsModel;
            collection.Version = _appSettings.SupportedVersion;
            sessionModel.Collection = collection;
            return sessionModel;
        }

        private SessionModel DecorateTopicsResponse(Session session)
        {
            var sessionModel = new SessionModel();
            var collectionModel = new CollectionModel();
            var lstItemsModel = new List<ItemsModel>();            
            
            foreach (var topic in session.TopicDetails) 
            {
                var itemModel = new ItemsModel
                {
                    Href = _linkGenerator.GetUriByRouteValues(HttpContext, "GetTopicById", new { id = topic.TopicId })
                };
                var lstDataModel = new List<DataModel>();
                var lstLinkModel = new List<LinkModel>();
                var datamodel = new DataModel
                {
                    Name = _appSettings.Title,
                    Value = topic.Details
                };
                lstDataModel.Add(datamodel);
                var linkmodel = new LinkModel
                {
                    Href = _linkGenerator.GetUriByRouteValues(HttpContext, "GetTopicforSession", new { id = topic.TopicId }),
                    Rel = _appSettings.SesssionLink
                };
                lstLinkModel.Add(linkmodel);
                itemModel.Data = lstDataModel;
                itemModel.Links = lstLinkModel;
                lstItemsModel.Add(itemModel);
            }
           collectionModel.Items = lstItemsModel;
           collectionModel.Version = _appSettings.SupportedVersion;
           sessionModel.Collection = collectionModel;
           return sessionModel;
        }
    }
}
