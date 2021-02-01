using ConferenceAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceAPI.Data
{
    public interface ISessionRepository
    {
        public Task<Session> GetSessionById(int id);
        public Task<List<Session>> GetAllSessions(string speakerame = "", int dayno = 0, string keyword = "");
        public Task<Session> GetTopicsBySessionId(int id);
    }
}
