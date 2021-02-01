using System.Collections.Generic;

namespace ConferenceAPI.Data.Entities
{
    public class Session
    { 
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionDetails { get; set; }  
        public Speaker SpeakerDetails { get; set; }
        public List<Topic> TopicDetails { get; set; }
        public TimeSlot TimeSlotDetails { get; set; }

    }
}
