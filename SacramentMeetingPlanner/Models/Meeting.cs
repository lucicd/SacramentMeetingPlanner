using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentMeetingPlanner.Models
{
    public class Meeting
    {
        public int ID { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = false)]
        [Display(Name = "Meeting Date")]
        public DateTime MeetingDate { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Conducting { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Invocation { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string OpeningSong { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        [Display(Name = "Sacrament Song")]
        public string SacramentSong { get; set; }

        [StringLength(255, MinimumLength = 2)]
        [Display(Name = "Intermediate Song")]
        public string IntermediateSong { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        [Display(Name = "Closing Song")]
        public string ClosingSong { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Benediction { get; set; }

        public ICollection<Speaker> Speakers { get; set; }
    }
}
