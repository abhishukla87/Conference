namespace ConferenceAPI.Data.Entities
{
    public class Speaker
    {    
        public int SpeakerId { get; set; }
        public string Name { get; set; }        
        public string Details { get; set; }
        public string SpeakerImagePath { get; set;}
        public string SpeakerImageType { get; set; }
    }
}
