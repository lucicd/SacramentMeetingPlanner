using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Models
{
    public class Meeting
    {
        public int ID { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Conducting { get; set; }
        public string Invocation { get; set; }
        public string OpeningSong { get; set; }
        public string SacramentSong { get; set; }
        public string IntermediateSong { get; set; }
        public string ClosingSong { get; set; }
        public string Benediction { get; set; }

        public ICollection<Speaker> Speakers { get; set; }
    }
}
