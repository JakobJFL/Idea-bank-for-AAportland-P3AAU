using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib
{
    public class NewIdea : IIdea
    {
        [Required (ErrorMessage = "Projekt Navn skal udfyldes")]
        [StringLength(100, ErrorMessage = "Projektnavn må max være 100 tegn")]
        public string ProjectName { get; set; }

        [Required (ErrorMessage = "Initialer skal udfyldes.")]
        [StringLength(5, ErrorMessage = "Initialer må max være 5 tegn")]
        public string Initials { get; set; }
        [Required]
        public string Description { get; set; }
        public int BusinessUnit { get; set; }
        public int Department { get; set; }
        public string Team { get; set; }
        public string Plan { get; set; }
        public bool IsHidden { get; set; }
        public string ExpectedResults { get; set; }
    }
}
