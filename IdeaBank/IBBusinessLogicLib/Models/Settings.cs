using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib.Models
{
    public class Settings
    {
        public bool CommentsEnabled { get; set; } = true;
        [StringLength(1000)]
        public string Purpose { get; set; }
        [StringLength(1000)]
        public string HomepageGuide { get; set; }
        [StringLength(1000)]
        public string SubmitGuide { get; set; }
    }
}