using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib.Models
{
    public class Idea
    {
        [Required]
        [StringLength(75, ErrorMessage = "Projektnavn må max være 75 tegn")]
        public string ProjectName { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "Initialer må max være 5 tegn")]
        public string Initials { get; set; }
        [Required]
        [StringLength(1500, ErrorMessage = "Beskrivelsen må max være 1500 tegn")]
        public string Description { get; set; }
        public string Team { get; set; }
        public string Plan { get; set; }
        public string ExpectedResults { get; set; }
        public bool IsHidden { get; set; }
        public int? AuthorBusinessUnit { get; set; } = 1;
        public int? AuthorDepartment { get; set; } = 1;
        public int? IdeaBusinessUnit { get; set; } = 1;
        public int? IdeaDepartment { get; set; } = 1;
        public int Priority { get; set; } = 1;
        public int Status { get; set; } = 1;
    }
}
