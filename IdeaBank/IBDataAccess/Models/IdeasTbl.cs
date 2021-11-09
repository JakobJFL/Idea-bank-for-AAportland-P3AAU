using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLib.Models
{
    public class IdeasTbl
    {
        [Key]
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
        [MaxLength(500)]
        public string Risk { get; set; }
        [MaxLength(75)]
        public string Team { get; set; }
        [MaxLength(500)]
        public string PlanDescription { get; set; }
        [MaxLength(500)]
        public string ExpectedResults { get; set; }
        [Required]
        public bool IsHidden { get; set; }
        public int Priority { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public BusinessUnitsTbl BusinessUnit { get; set; }
        public DepartmentsTbl Department { get; set; }
    }
}
