using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLib.Models
{
    public class CommentsTbl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string Initials { get; set; }

        [Required]
        [MaxLength(1500)]
        [Column(TypeName = "varchar(1000)")]
        public string Message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public IdeasTbl Idea { get; set; }
    }
}
