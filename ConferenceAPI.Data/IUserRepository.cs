using ConferenceAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceAPI.Data
{
    public interface IUserRepository
    {
        User GetByUserName(string userdetail);
    }
}
