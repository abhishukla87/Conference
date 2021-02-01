using ConferenceAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceAPI.Data
{
    public interface ITopicRepository
    {
        public Topic GetTopic(int id);
        public Topic[] GetTopics(int dayno = 0);
        public Topic GetTopicSessions(int id = 0);
        public Topic GetTopicSpeaker(int id = 0);
    }
}
