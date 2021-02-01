using ConferenceAPI.Data.Entities;
using System;

namespace ConferenceAPI.Data
{
    public class MockSpeakerRepository : ISpeakerRepository
    {
        public Speaker GetAllSpeakers(string speakerame = "", int dayno = 0, string keyword = "")
        {
            throw new NotImplementedException();
        }

        public Speaker GetSpeakerSessions(int id)
        {
            throw new NotImplementedException();
        }

        public Speaker GetSpeakerTopics(int id)
        {
            throw new NotImplementedException();
        }

        public Speaker GetSpecker(int id)
        {
            throw new NotImplementedException();
        }
    }
}
