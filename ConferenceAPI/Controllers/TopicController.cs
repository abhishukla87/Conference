using ConferenceAPI.Data;
using ConferenceAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ConferenceAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("conference")]
    [ApiVersion("1.0")]
    public class TopicController : ControllerBase
    {  
        private readonly ITopicRepository _topicRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly AppSettings _appSettings;

        public TopicController(ITopicRepository topicrepository, 
                               LinkGenerator linkGenerator,
                               AppSettings appSettings)
        {
            _topicRepository = topicrepository;
            _linkGenerator = linkGenerator;
            _appSettings = appSettings;
        }

        [Route("topic/{id:int}" , Name = "GetTopicById")]
        [HttpGet]
        public ActionResult GetTopic(int id)
        {
            //Not implemented 
            return Ok();
        }

        [Route("topics")]
        [HttpGet("{dayno:int}")]
        public ActionResult Gettopics(int dayno = 0)
        {
            //Not implemented 
            return Ok();
        }

        [Route("topics/{id}/sessions", Name= "GetTopicforSession")]
        [HttpGet("{id:int}")]
        public ActionResult GetTopicSessions(int id = 0)
        {
            //Not implemented 
            return Ok();
        }

        
        [Route("topics/{id}/speakers"  )]
        [HttpGet("{id:int}")]
        public ActionResult GetTopicSpeaker(int id = 0)
        {
            //Not implemented 
            return Ok();
        }
    }
}
