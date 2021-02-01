using ConferenceAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceAPI.Data
{
    public class MockSessionRepository : ISessionRepository
    {       

        List<Session> _sessions = new List<Session> { 
             new Session { 
               SessionId =1,
               SessionName ="Async in C# - Session 1",
               SessionDetails = "Session details 1",              
               SpeakerDetails = new Speaker { 
               SpeakerId =1,
               Details ="This is speaker-1",
               Name = "Jon Skeet"
               },
               TimeSlotDetails = new TimeSlot { 
               TimeSlotId =1,
               TimeSlotTime ="04 December 2013 11:40 - 12:40",
               Day =1
               },
               TopicDetails = new List<Topic> { 
                new Topic { 
                TopicId =1,
                Details ="Topic 1"
                },
                new Topic {
                TopicId =2,
                Details ="Topic 2"
                }
               }
             },
             new Session {
               SessionId =2,
               SessionName ="Introducing Visual Studio 2013 Session 2",
               SessionDetails = "Session deatils 2",             
               SpeakerDetails = new Speaker {
               SpeakerId =2,
               Details ="This is speaker-2",
               Name = "Venkat Subramaniam"
               },
               TimeSlotDetails = new TimeSlot {
               TimeSlotId =2,
               TimeSlotTime ="05 December 2013 11:40 - 12:40",
               Day =2
               },
               TopicDetails = new List<Topic> {
                new Topic {
                TopicId =3,
                Details ="Topic 3"
                },
                new Topic {
                TopicId =2,
                Details ="Topic 2"
                }
               }
             }
             };

        public async Task<List<Session>> GetAllSessions(string speakerame = "", int dayno = 0, string keyword = "")
        {
            return  _sessions;
        }

        public async Task<Session> GetSessionById(int id) {

           return _sessions.FirstOrDefault(t => t.SessionId == id);
        } 

        public async Task<Session> GetTopicsBySessionId(int id)
        {
            return _sessions.FirstOrDefault(t => t.SessionId == id);
        }
    }
}
