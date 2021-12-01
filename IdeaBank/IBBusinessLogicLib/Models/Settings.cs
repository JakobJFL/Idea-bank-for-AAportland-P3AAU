namespace BusinessLogicLib.Models
{
    public class Settings
    {
        public bool CommentsEnabled { get; set; } = true;
        public string Purpose { get; set; }
        public string HomepageGuide { get; set; }
        public string SubmitGuide { get; set; }
    }
}