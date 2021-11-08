using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib
{
    public abstract class Idea
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
    }
}