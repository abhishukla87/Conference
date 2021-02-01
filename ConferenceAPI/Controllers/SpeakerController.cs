using ConferenceAPI.Data;
using ConferenceAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace ConferenceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("conference")]
    [ApiVersion("1.0")]
    public class SpeakerController : ControllerBase
    {
        private readonly ISpeakerRepository _speakerRepository;
        private readonly LinkGenerator _linkGenerator;
        private readonly AppSettings _appSettings;

        public SpeakerController(ISpeakerRepository speakerrepository, 
                                 LinkGenerator linkGenerator ,
                                 IOptions<AppSettings> appSettings
                                 )
        {
            _speakerRepository = speakerrepository;
            _linkGenerator = linkGenerator;
            _appSettings = appSettings.Value;
        }
    
        [Route("speaker/{id:int}", Name = "GetSpeakerById")]
        [HttpGet]
        public ActionResult GetSpeaker(int id)
        {
            //Not implemented  
            return Ok();
           
        }
        [Route("speakers")]
        [HttpGet("{dayno:int},{speakername}")]
        public ActionResult GetAllSpeakers(string speakerame = "", int dayno = 0, string keyword = "")
        {
            //Not implemented 
            return Ok();
        }

        [Route("speaker/{id:int}/topics")]
        [HttpGet]
        public ActionResult GetSpeakerTopics(int id)
        {
            //Not implemented 
            return Ok();
        }
    }
}
