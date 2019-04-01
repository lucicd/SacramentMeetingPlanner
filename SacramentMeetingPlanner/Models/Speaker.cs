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
        public string SpeakerName { get; set; }
        public string Subject { get; set; }
        public Block Block { get; set; }
        public int Order { get; set; }

        public Meeting Meeting { get; set; }
    }
}
