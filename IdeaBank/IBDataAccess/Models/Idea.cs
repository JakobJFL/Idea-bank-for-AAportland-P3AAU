using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBDataAccessLib.Models
{
    public class Idea
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; }
        [Required]
        [MaxLength(5)]
        public string Initials { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        public BusinessUnit BusinessUnit { get; set; } = new();
        public Department Department { get; set; } = new();
        [MaxLength(1000)]
        public string Risk { get; set; }
        [MaxLength(100)]
        public string Team { get; set; }
        [MaxLength(1000)]
        public string PlanDescription { get; set; }
        [MaxLength(1000)]
        public string ExpectedResults { get; set; }
        [Required]
        public bool IsHidden { get; set; } = false;
        public int Priority { get; set; }
        [Required]
        public int Status { get; set; } = 1;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Comment> Comments { get; set; } = new();
    }
}
