using System.ComponentModel.DataAnnotations;

namespace SacramentMeetingPlanner.Models
{
    public enum Block
    {
        FIRST, SECOND
    }

    public class Speaker
    {
        public int ID { get; set; }
        public int MeetingID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        [Display(Name = "Speaker Name")]
        public string SpeakerName { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Subject { get; set; }

        [Required]
        public Block Block { get; set; }

        [Required]
        public int Order { get; set; }

        public Meeting Meeting { get; set; }
    }
}
