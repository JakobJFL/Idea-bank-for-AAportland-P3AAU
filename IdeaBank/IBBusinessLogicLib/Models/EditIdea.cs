using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLib.Models
{
    public class EditIdea : Idea
    {
        [Required]
        public int Id { get; set; }
        [StringLength(500)]
        public string Risk { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
