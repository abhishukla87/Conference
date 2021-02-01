using ConferenceAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceAPI.Data
{
    public interface ISpeakerRepository
    {
        public Speaker GetSpecker(int id);
        public Speaker GetAllSpeakers(string speakerame = "", int dayno = 0, string keyword = "");
        public Speaker GetSpeakerSessions(int id);
        public Speaker GetSpeakerTopics(int id);

    }
}
