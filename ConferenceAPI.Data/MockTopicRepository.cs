using ConferenceAPI.Data.Entities;
using System;


namespace ConferenceAPI.Data
{
    public class MockTopicRepository : ITopicRepository
    {
        public Topic GetTopic(int id)
        {
            throw new NotImplementedException();
        }

        public Topic[] GetTopics(int dayno = 0)
        {
            throw new NotImplementedException();
        }

        public Topic GetTopicSessions(int id = 0)
        {
            throw new NotImplementedException();
        }

        public Topic GetTopicSpeaker(int id = 0)
        {
            throw new NotImplementedException();
        }
    }
}
