using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceAPI.Models
{
    public class SpeakerModel
    {
        public int SpeakerId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string SpeakerImagePath { get; set; }
        public string SpeakerImageType { get; set; }
    }
}
