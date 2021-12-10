using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib.Models
{
    public class Idea
    {
        [Required]
        [StringLength(75)]
        public string ProjectName { get; set; }
        [Required]
        [StringLength(5)]
        public string Initials { get; set; }
        [Required]
        [StringLength(1500)]
        public string Description { get; set; }
        [StringLength(75)]
        public string Team { get; set; }
        [StringLength(500)]
        public string Plan { get; set; }
        [StringLength(500)]
        public string ExpectedResults { get; set; }
        [Required]
        public bool IsHidden { get; set; }
        public int? AuthorBusinessUnit { get; set; } = 1;
        public int? AuthorDepartment { get; set; } = 1;
        public int? IdeaBusinessUnit { get; set; } = 1;
        public int? IdeaDepartment { get; set; } = 1;
        [Required]
        public byte Priority { get; set; } = 1;
        [Required]
        public byte Status { get; set; } = 1;
    }
}
