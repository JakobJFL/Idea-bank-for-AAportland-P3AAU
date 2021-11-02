using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "Initialer må max indeholde 5 tegn")]
        public string Initials { get; set; }
        [Required]
        [StringLength (1500, ErrorMessage = "Kommentar må max indeholde 1500 tegn")]
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdeaId { get; set; }

    }
}
