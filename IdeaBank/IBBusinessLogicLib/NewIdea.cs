using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib
{
    public class NewIdea : IIdea
    {
        [Required]
        [StringLength(100, ErrorMessage = "Projektnavn må max være 100 tegn")]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Initialer må max være 5 tegn")]
        public string Initials { get; set; }

        [Required]
        [StringLength(1500, ErrorMessage = "Beskrivelsen må max være 1500 tegn")]
        public string Description { get; set; }
        public int BusinessUnit { get; set; } = 1;
        public int Department { get; set; } = 1;
        public string Team { get; set; }
        public string Plan { get; set; }
        public bool IsHidden { get; set; }
        public string ExpectedResults { get; set; }
    }
}
