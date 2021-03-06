﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models.MeetingViewModels
{
    public class MeetingIndexData
    {
        public PaginatedList<Meeting> Meetings { get; set; }
        public IEnumerable<Speaker> Speakers { get; set; }
    }
}
