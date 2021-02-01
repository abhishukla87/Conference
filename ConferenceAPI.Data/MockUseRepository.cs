using ConferenceAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceAPI.Data
{
    public class MockUseRepository : IUserRepository
    {
       
        private List<User> _users = new List<User>
        {
            new User {
                Id = 1, 
                FirstName = "Test",
                LastName = "User",
                Username = "test" 
            }
        };

        public User GetByUserName(string userdetail)
        {
            return _users.FirstOrDefault();
        }
    }
}
